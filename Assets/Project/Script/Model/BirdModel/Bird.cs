using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Audio;
using Farme.Tool;
namespace Bird_VS_Boar
{ 
    public abstract class Bird : BaseMono,IBoom
    {
        /// <summary>
        /// 音效
        /// </summary>
        protected Audio m_Effect = null;
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
        /// 小鸟配置信息
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
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            RecyclyAudio();//回收音效
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
            GameLogic.NowComeBird = null;//断开引用
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
                MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,OnBirdFlyUpdate_Common);
            }
            //小鸟面朝飞行方向
            transform.eulerAngles = new Vector3(0, 0, -Vector2.SignedAngle(m_Rig2D.velocity.normalized, Vector2.right));
            if (m_Rig2D.velocity.magnitude > 0.5f)
            {
                if (Physics2D.OverlapCircle(transform.position, m_CC2D.radius, LayerMask.GetMask(rayCastGroup)))
                {                  
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,OnBirdFlyUpdate_Common);
                    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,OnSkillUpdate_Common);
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
            MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(3.0f,()=> 
            {
                OpenBoom(); //打开死亡特效
                PlayDestroyAudio();//播放销毁音效
                GoReusePool.Put(GetType().Name, gameObject);//回收小鸟               
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
        #region Audio   默认->选中->飞行->碰撞->销毁     
        /// <summary>
        /// 播放飞行音效
        /// </summary>
        protected virtual void PlayFlyAudio()
        {
            if(!BirdConfigInfo.BirdConfigInfoDic.TryGetValue(GetType().Name, out var config))
            {
                return;
            }
            PlayAudio(config.GetFlyAudioPaths());
        }
        /// <summary>
        /// 播放选中音效
        /// </summary>
        protected virtual void PlaySelectAudio()
        {
            if (!BirdConfigInfo.BirdConfigInfoDic.TryGetValue(GetType().Name, out var config))
            {
                return;
            }
            PlayAudio(config.GetSelectAudioPaths());
        }
        /// <summary>
        /// 播放销毁音效
        /// </summary>
        protected virtual void PlayDestroyAudio()
        {
            if (!BirdConfigInfo.BirdConfigInfoDic.TryGetValue(GetType().Name, out var config))
            {
                return;
            }
            PlayAudio(config.GetDiedAudioPath());
        }
        /// <summary>
        /// 播放碰撞音效
        /// </summary>
        protected virtual void PlayCollisionAudio()
        {
            if (!BirdConfigInfo.BirdConfigInfoDic.TryGetValue(GetType().Name, out var config))
            {
                return;
            }
            PlayAudio(config.GetCollisionAudioPaths());
        }
        /// <summary>
        /// 播放技能音效
        /// </summary>
        protected virtual void PlaySkillAudio()
        {
            
        }
        protected void PlayAudio(string audioPath)
        {
            if (audioPath == null)
            {
                Debuger.LogWarning(GetType().Name + ": 存在无效的音效路径，请查看对应配置表");
                return;
            }
            ApplyAudio();
            if(AudioClipManager.GetAudioClip(audioPath,out AudioClip clip))
            {
                m_Effect.Clip = clip;
                m_Effect.Play();
            }
        }
        private void ApplyAudio()
        {
            if(m_Effect==null)
            {
                m_Effect = AudioManager.ApplyForAudio();
                m_Effect.SpatialBlend = 0;//设置为2D
                m_Effect.AbleRecycle = false;//不可自动回收
                m_Effect.Group = AudioMixerManager.GetAudioMixerGroup("Effect");
            }
        }     
        private void RecyclyAudio()
        {
            if(m_Effect!=null)
            {
                m_Effect.AbleRecycle = true;
                m_Effect = null;
            }
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
                if(!NotMonoSingletonFactory<OtherConfigInfo>.SingletonExist)
                {
                    return;
                }
                if (!GoLoad.Take(NotMonoSingletonFactory<OtherConfigInfo>.GetSingleton().GetBoomPrefabPath(), out go))
                {
                    return;
                }
            }
            go.transform.position = transform.position;
            go.GetComponent<Boom>().OpenBoom(ENUM_BoomType.BirdBoom);
        }
        #endregion     
    }
}
