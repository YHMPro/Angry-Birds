using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using UnityEngine.Audio;
namespace Bird_VS_Boar
{ 
    public abstract class Bird : BaseMono
    {
        /// <summary>
        /// 是否能绑定鸟巢
        /// </summary>
        protected bool m_IsAbleBindBirdNets = false;
        /// <summary>
        /// 是否选中
        /// </summary>
        protected bool m_IsCheck = false;      
        /// <summary>
        /// 是否释放技能
        /// </summary>
        protected bool m_IsReleaseSkill = false;        
        /// <summary>
        /// 配置
        /// </summary>
        protected BirdConfig m_Config = null;
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
        protected override void Awake()
        {
            base.Awake();
            m_CC2D = gameObject.AddComponent<CircleCollider2D>();
            m_Rig2D = GetComponent<Rigidbody2D>();
            m_Anim = GetComponent<Animator>();
            m_TRenderer = GetComponent<TrailRenderer>();
            drawLineEnd = transform.Find("DrawLineEnd");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ActiveTrailRenderer(false);
            m_Anim.SetTrigger("IsDefault");
            m_IsReleaseSkill = false;
            m_IsCheck = false;
            IsFreeze_ZRotation = true;
            transform.eulerAngles = Vector3.forward * 25;
        }      
        protected override void Start()
        {
            base.Start();
            InitTrailRenderer();//初始化拖尾
            m_Config.InitResources();//初始化资源路径
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
        protected virtual void OnMouseEnter()
        {
            
        }

        protected virtual void OnMouseDown()
        {
            if (!GameLogic.BirdIsNowComeBirdLogic(this))
                return;
            m_IsCheck = true;
            if (MonoSingletonFactory<SlingShot>.SingletonExist)
            {
                MonoSingletonFactory<SlingShot>.GetSingleton().PlaySlingShotAudio();
            }
            if (MonoSingletonFactory<FlyPath>.SingletonExist)
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
                slingShot.PauseSlingShotAudio();
                slingShot.BreakBird();//断开关联
                slingShot.ClearLine();//清除线
            }
            PlayFlyAudio();//播放飞行音效
            m_Anim.SetTrigger("IsFly");//飞行动画
            ActiveTrailRenderer(true);//开启拖尾
        }

        protected virtual void OnMouseExit()
        {
            
        }
        #region Update
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
        /// <summary>
        /// 监听小鸟飞行更新
        /// </summary>
        public void OnBirdFlyUpdate_Common()//用于处理鸟的飞行后的首次碰撞  
        {            
            if(!gameObject.activeInHierarchy)
            {
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(OnBirdFlyUpdate_Common);
            }
            //小鸟面朝飞行方向
            transform.eulerAngles = new Vector3(0, 0, -Vector2.SignedAngle(m_Rig2D.velocity.normalized, Vector2.right));
            if (m_Rig2D.velocity.magnitude > 0.5f)
            {
                if (Physics2D.OverlapCircle(transform.position, m_CC2D.radius + 0.15f, LayerMask.GetMask(rayCastGroup)))
                {                   
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(OnBirdFlyUpdate_Common);
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(OnSkillUpdate_Common);
                    ActiveTrailRenderer(false);//关闭拖尾
                    PlayCollisionAudio();//播放碰撞音效
                    OnBirdFlyUpdate();                 
                }
            }         
        }
        /// <summary>
        /// 监听小鸟飞行更新
        /// </summary>
        protected virtual void OnBirdFlyUpdate()
        {
            m_Anim.SetTrigger("IsHurt");//受伤动画
            MonoSingletonFactory<ShareMono>.GetSingleton().DelayUAction(3.0f,()=> 
            {
                OpenBoom(); //打开死亡特效
                PlayDestroyAudio();//播放销毁音效
                GoReusePool.Put(GetType().Name, gameObject);//回收小鸟
                GameLogic.NowComeBird = null;//断开引用
            });
        }
        #endregion
        #region SKill
        /// <summary>
        /// 监听技能更新
        /// </summary>
        public virtual void OnSkillUpdate_Common()
        {
            
        }
        /// <summary>
        /// 监听技能更新
        /// </summary>
        protected virtual void OnSkillUpdate()
        {

        }
        #endregion
        #region Velocity
        /// <summary>
        /// 设置小鸟飞行速度
        /// </summary>
        /// <param name="velocity">速度</param>
        public virtual void SetBirdFlyVelocity(Vector2 velocity)
        {
            m_Rig2D.velocity = velocity;
        }      
        #endregion
        #region Audio
        /// <summary>
        /// 播放飞行音效
        /// </summary>
        protected virtual void PlayFlyAudio()
        {
            GameAudio.PlayBirdAudio(m_Config.GetFlyAudioPath());
        }
        /// <summary>
        /// 播放选中音效
        /// </summary>
        protected virtual void PlaySelectAudio()
        {
            GameAudio.PlayBirdAudio(m_Config.GetSelectAudioPath());
        }
        /// <summary>
        /// 播放销毁音效
        /// </summary>
        protected virtual void PlayDestroyAudio()
        {
            GameAudio.PlayBirdAudio(m_Config.GetDestroyedAudioPath());
        }
        /// <summary>
        /// 播放碰撞音效
        /// </summary>
        protected virtual void PlayCollisionAudio()
        {
            GameAudio.PlayBirdAudio(m_Config.GetCollisionAudioPath());
        }
        /// <summary>
        /// 播放技能音效
        /// </summary>
        protected virtual void PlaySkillAudio()
        {
           
        }
        #endregion
        #region TrailRenderer
        /// <summary>
        /// 拖尾活动状态
        /// </summary>
        /// <param name="active"></param>
        public virtual void ActiveTrailRenderer(bool active)
        {
            if(m_TRenderer!=null)
            {
                m_TRenderer.enabled = active;
            }
        }
        /// <summary>
        /// 初始化拖尾
        /// </summary>
        protected virtual void InitTrailRenderer()
        {
            m_TRenderer.startWidth = 0.15f;
            m_TRenderer.endWidth = 0.05f;
            m_TRenderer.time = 0.5f;
            m_TRenderer.startColor = Color.white;
            m_TRenderer.endColor = new Color32(0, 0, 0, 0);            
        }
        #endregion
        #region Boom
        public virtual void OpenBoom()
        {
            GameObject go;
            if(!GoReusePool.Take(typeof(Boom).Name,out go))
            {
                if (!GoLoad.Take(m_Config.BoomPath, out go))
                {
                    return;
                }
            }
            go.transform.position = transform.position;
            go.GetComponent<Boom>().OpenBoom("BirdBoom");
        }
        #endregion     
    }
}
