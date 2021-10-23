using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    /// <summary>
    /// 弹弓
    /// </summary>
    public class SlingShot : BaseMono
    {
        private SpringJoint2D m_SJ2D;

        private float m_ApplyingMaxSpeed = 15;
        public float StretchDis
        {
            get { return 1; }
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
                    return birdToSelfDir.normalized * birdToSelfDir.magnitude / StretchDis * m_ApplyingMaxSpeed;
                }
                return Vector2.zero;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            RegisterComponentsTypes<LineRenderer>();

            m_SJ2D = GetComponent<SpringJoint2D>();
        }

        protected override void Start()
        {
            base.Start();
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
            m_SJ2D.connectedBody = GameLogic.NowComeBird.GetComponent<Rigidbody2D>();//设置刚体绑定链接
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(GameLogic.NowComeBird.BirdControlUpdate);//添加小鸟控制更新

        }

        public void BreakBird()
        {
            if (GameLogic.NowComeBird == null)
                return;
            m_SJ2D.connectedBody = null;
            MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(GameLogic.NowComeBird.BirdControlUpdate);//移除小鸟控制更新
            MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(GameLogic.NowComeBird.BirdFlyUpdate);//添加小鸟飞行更新
            GameLogic.NowComeBird.SetBirdRig2DVelocity(ApplyingVelocity);//设置小鸟基于弹弓获得的初始速度
            GameLogic.NowComeBird.IsFreeze_ZRotation = false;//解除小鸟Z轴选中冻结
            
            //计算预瞄准点位置
            if (MonoSingletonFactory<FlyPath>.SingletonExist)
            {
                FlyPath flyPath = MonoSingletonFactory<FlyPath>.GetSingleton();
                flyPath.SetFlyPath(0.2f,0.2f);
                MonoSingletonFactory<ShareMono>.GetSingleton().DelayUAction(2, () =>
                 {
                     if(flyPath!=null)
                     {
                         flyPath.ActiveFlyPath(false);
                     }
                 });
            }          
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
