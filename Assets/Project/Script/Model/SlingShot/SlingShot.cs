using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Audio;
using Farme.Tool;
using Bird_VS_Boar.LevelConfig;

namespace Bird_VS_Boar
{
    /// <summary>
    /// 弹弓
    /// </summary>
    public class SlingShot : MonoSingletonBase<SlingShot>
    {
        private AudioSource m_As;
        private SpringJoint2D m_SJ2D;
        private float m_ApplyingMaxSpeed = 15.0f;
        public float StretchDis
        {
            get { return 1.0f; }
        }
        public bool SJ2DEnable
        {
            set
            {
                m_SJ2D.enabled = value;
            }
            get
            {
                return m_SJ2D.enabled;
            }
        }

        public Vector2 ApplyingVelocity
        {
            get
            {
                if(GameLogic.NowComeBird!=null)
                {
                    Vector2 birdToSelfDir = GetOriginGlobalPos(GameLogic.NowComeBird.transform.position.z) - GameLogic.NowComeBird.transform.position;
                    return birdToSelfDir.normalized * (birdToSelfDir.magnitude / StretchDis * m_ApplyingMaxSpeed);
                }
                return Vector2.zero;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<LineRenderer>();
            m_As=GetComponent<AudioSource>();          
            m_SJ2D = GetComponent<SpringJoint2D>();
        }
        protected override void Start()
        {
            base.Start();
            if (OtherConfigInfo.Exists)
            {
                if(AudioClipManager.GetAudioClip(OtherConfigInfo.GetSingleton().GetSlingShotAudioPath(),out AudioClip clip))
                {
                    m_As.clip = clip;
                }
            }
            m_As.outputAudioMixerGroup = AudioMixerManager.GetAudioMixerGroup("Effect");

            if (GetComponent("LeftRendererLine", out LineRenderer leftLR))
            {
                leftLR.positionCount = 0;
                leftLR.startWidth = 0.2f;
                leftLR.endWidth = 0.1f;
            }
            if (GetComponent("RightRendererLine", out LineRenderer rightLR))
            {
                rightLR.positionCount = 0;
                rightLR.startWidth = 0.2f;
                rightLR.endWidth = 0.1f;
            }                    
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            //读取关卡配置信息来设置位置
            Debuger.Log("读取关卡配置信息来设置位置");
            LevelConfig.LevelConfig levelConfig = LevelConfigManager.GetLevelConfig(GameManager.NowLevelType + "_" + GameManager.NowLevelIndex);
            if (levelConfig == null)
            {
                Debuger.LogError("不存在此场景的配置");
                return;
            }
            transform.position = levelConfig.SlingShotPosition.ToVector3();
        }

        public void RendererLine(Vector3 drawLineEndPoint)
        {
            if (GetComponent("LeftRendererLine",out LineRenderer leftLR))
            {
                if(leftLR.positionCount!=2)
                {
                    leftLR.positionCount = 2;
                }
                leftLR.SetPosition(0, leftLR.transform.position);
                leftLR.SetPosition(1, drawLineEndPoint);
            }
            if(GetComponent("RightRendererLine", out LineRenderer rightLR))
            {
                if (rightLR.positionCount != 2)
                {
                    rightLR.positionCount = 2;
                }
                rightLR.SetPosition(0, rightLR.transform.position);
                rightLR.SetPosition(1, drawLineEndPoint);
            }
        }
        public void ClearLine()
        {
            if (GetComponent("LeftRendererLine", out LineRenderer leftLR))
            {
                leftLR.positionCount = 0;
                Debuger.Log(leftLR.positionCount);
            }
            if (GetComponent("RightRendererLine", out LineRenderer rightLR))
            {
                rightLR.positionCount = 0;
            }
        }
        public void BindBird()
        {
            if (GameLogic.NowComeBird == null)
                return;
            GameLogic.NowComeBird.transform.position = GetOriginGlobalPos(GameLogic.NowComeBird.transform.position.z);
            m_SJ2D.connectedBody = GameLogic.NowComeBird.GetComponent<Rigidbody2D>();//设置刚体绑定链接
            GameLogic.NowComeBird.AddBirdControlUpdate();
        }

        public void BreakBird()
        {
            if (GameLogic.NowComeBird == null)
                return;
            m_SJ2D.connectedBody = null;
            if(Camera2D.Exists)
            {
                Camera2D.GetSingleton().BindBird();
            }
            GameLogic.NowComeBird.AddOnBirdFlyUpdate_Common();//添加小鸟飞行更新
            GameLogic.NowComeBird.SetBirdFlyVelocity(ApplyingVelocity);//设置小鸟基于弹弓获得的初始速度
            GameLogic.NowComeBird.IsFreeze_ZRotation = false;//解除小鸟Z轴选中冻结       
            //计算预瞄准点位置
            if (FlyPath.Exists)
            {
                FlyPath flyPath = FlyPath.GetSingleton();
                flyPath.SetFlyPath(0.2f,0.2f);
                flyPath.ActiveFlyPath(false);               
            }          
        }
        public void PlaySlingShotAudio()
        {
            m_As.Play();
        }
        public void PauseSlingShotAudio()
        {
            m_As.Stop();
        }
        public Vector3 GetOriginGlobalPos(float z)
        {
            Vector3 origin = transform.position + (Vector3)m_SJ2D.anchor;
            origin.z = z;
            return origin;
        }

        public Vector2 CountPathPoint(float time)
        {
            Vector2 v = ApplyingVelocity;
            Vector2 pos = Vector2.zero;
            pos.x = v.x * time;
            pos.y = v.y * time + 0.5f * Physics2D.gravity.y * time * time;
            return pos;
        }
        
    }
}
