using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using UnityEngine.Audio;
namespace Angry_Birds
{ 
    public abstract class Bird : BaseMono
    {            

        /// <summary>
        /// 是否选中
        /// </summary>
        private bool m_IsCheck = false;      
        /// <summary>
        /// 是否释放技能
        /// </summary>
        private bool m_IsReleaseSkill = false;
        /// <summary>
        /// 是否受伤
        /// </summary>
        protected bool m_IsHurt = false;
        /// <summary>
        /// 配置信息
        /// </summary>
        protected BirdConfigInfo m_ConfigInfo = null;
        /// <summary>
        /// 碰撞盒子
        /// </summary>
        protected CircleCollider2D m_CC2D = null;
        /// <summary>
        /// 刚体
        /// </summary>
        protected Rigidbody2D m_Rig2D = null;
        /// <summary>
        /// 动画状态机
        /// </summary>
        protected Animator m_Anim = null;
        /// <summary>
        /// 拖尾
        /// </summary>
        protected TrailRenderer m_TRenderer = null;
        /// <summary>
        /// 绘制线结束点
        /// </summary>
        protected Transform drawLineEnd;
        /// <summary>
        /// 射线检测的组
        /// </summary>
        protected string[] rayCastGroup = new string[] { "Pig", "Barrier","Land" };      
        /// <summary>
        /// 是否冻结Z轴选中
        /// </summary>
        public bool IsFreeze_ZRotation
        {
            set
            {
                if(m_Rig2D==null)
                {
                    m_Rig2D = GetComponent<Rigidbody2D>();
                }
                m_Rig2D.freezeRotation = value;
            }
            get
            {
                if (m_Rig2D == null)
                {
                    m_Rig2D = GetComponent<Rigidbody2D>();
                }
                return m_Rig2D.freezeRotation;
            }
        }
        /// <summary>
        /// 是否释放技能
        /// </summary>
        protected bool IsReleaseSkill
        {
            get
            {
                return m_IsReleaseSkill;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            m_CC2D = gameObject.AddComponent<CircleCollider2D>();
            m_Rig2D = GetComponent<Rigidbody2D>();
            m_Anim = GetComponent<Animator>();
            m_TRenderer = GetComponent<TrailRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            m_Rig2D.mass = m_ConfigInfo.Mass;
            m_Anim.SetTrigger("IsDefault");
            drawLineEnd = transform.Find("DrawLineEnd");

            SetTrailRenderer();//设置拖尾
        }
        protected virtual void OnMouseEnter()
        {
            
        }

        protected virtual void OnMouseDown()
        {
            if (!GameLogic.BirdIsNowComeBirdLogic(this))
                return;
            m_IsCheck = true;          
            if(MonoSingletonFactory<FlyPath>.SingletonExist)
            {
                MonoSingletonFactory<FlyPath>.GetSingleton().ActiveFlyPath(true);
            }
            PlaySelectAudio();//播放选择音效
        }

        protected virtual void OnMouseUp()
        {
            if (!GameLogic.BirdIsNowComeBirdLogic(this))
                return;
            m_IsCheck = false;
            if (MonoSingletonFactory<SlingShot>.SingletonExist)
            {
                SlingShot slingShot = MonoSingletonFactory<SlingShot>.GetSingleton();
                slingShot.BreakBird();//断开关联
                slingShot.ClearLine();//清除线
            }
            PlayFlyAudio();//播放飞行音效
            m_Anim.SetTrigger("IsFly");//飞行动画
            m_TRenderer.enabled = true;//开启拖尾
        }

        protected virtual void OnMouseExit()
        {
            
        }
        
        public void BirdControlUpdate()
        {
            if (m_IsCheck)//是否选中
            {
                if (MonoSingletonFactory<Camera2D>.SingletonExist)
                {
                    Vector3 movePos = MonoSingletonFactory<Camera2D>.GetSingleton().ScreenToWorldPoint(Input.mousePosition, transform.position.z);
                    if (MonoSingletonFactory<SlingShot>.SingletonExist)
                    {
                        SlingShot slingShot = MonoSingletonFactory<SlingShot>.GetSingleton();
                        Vector3 dir = movePos - slingShot.GetOriginGlobalPos(transform.position.z);                    
                        //限制拉伸的范围
                        transform.position = dir.magnitude <= slingShot.StretchDis ? movePos : dir.normalized * slingShot.StretchDis + slingShot.GetOriginGlobalPos(transform.position.z);
                        //渲染线
                        slingShot.RendererLine(drawLineEnd.position);
                        //计算预瞄准点位置                      
                        if (MonoSingletonFactory<FlyPath>.SingletonExist)
                        {
                            MonoSingletonFactory<FlyPath>.GetSingleton().SetFlyPath(0.2f, 0.2f);
                        }
                        //小鸟面朝发射方向
                        transform.eulerAngles = new Vector3(0, 0, -Vector2.SignedAngle(-dir.normalized, Vector2.right));
                    }             
                }
            }
            else
            {
                SlingShot slingShot = MonoSingletonFactory<SlingShot>.GetSingleton();
                //渲染线
                slingShot.RendererLine(drawLineEnd.position);
            }
        }

        public virtual void BirdFlyUpdate()//用于处理鸟的飞行后的首次碰撞  
        {
            //小鸟面朝飞行方向
            transform.eulerAngles = new Vector3(0, 0, -Vector2.SignedAngle(m_Rig2D.velocity.normalized, Vector2.right));
            if (m_Rig2D.velocity.magnitude > 0.5f)
            {
                if (Physics2D.OverlapCircle(transform.position, m_CC2D.radius + 0.15f, LayerMask.GetMask(rayCastGroup)))
                {
                    m_IsHurt = true;
                    PlayCrashAudio();//播放碰撞音效
                    m_Anim.SetTrigger("IsHurt");//受伤动画
                    m_TRenderer.enabled = false;//关闭拖尾            
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
                }
            }
            else
            {              
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(BirdFlyUpdate);
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(SkillUpdate);
            }
        }

        #region SKill
        protected virtual void SkillUpdate()
        {
            if (!m_IsReleaseSkill)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    m_Anim.SetTrigger("IsSkill");
                    m_IsReleaseSkill = true;
                }
            }
        }
        #endregion
        #region Rig
        public virtual void SetBirdRig2DVelocity(Vector2 velocity)
        {
            m_Rig2D.velocity = velocity;
        }      
        #endregion
        #region Audio
        protected virtual void PlayFlyAudio()
        {
            GameAudio.PlayBirdAudio(m_ConfigInfo.GetFlyAudioPath(true));
        }

        protected virtual void PlaySelectAudio()
        {
            GameAudio.PlayBirdAudio(m_ConfigInfo.GetSelectAudioPath(true));
        }

        protected virtual void PlayDiedAudio()
        {
            GameAudio.PlayBirdAudio(m_ConfigInfo.GetDiedAudioPath(true)); 
        }
        protected virtual void PlayCrashAudio()
        {
            GameAudio.PlayBirdAudio(m_ConfigInfo.GetCrashAudioPath(true));
        }
        
        protected virtual void PlaySkillAudio()
        {
            GameAudio.PlayBirdAudio(m_ConfigInfo.GetSkillAudioPath(true));
        }
        #endregion
        #region TrailRenderer
        protected virtual void SetTrailRenderer()
        {
            m_TRenderer.startWidth = 0.15f;
            m_TRenderer.endWidth = 0.05f;
            m_TRenderer.time = 0.5f;
            m_TRenderer.startColor = Color.white;
            m_TRenderer.endColor = new Color32(0, 0, 0, 0);
            m_TRenderer.enabled = false;
        }
        #endregion

    }
}
