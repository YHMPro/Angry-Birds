using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Audio;
using Farme.Tool;
using Farme.UI;
using UnityEngine.EventSystems;
namespace Bird_VS_Boar
{ 
    /// <summary>
    /// 小鸟类型
    /// </summary>
    public enum EnumBirdType
    {
        None,
        RedBird,
        BlackBird,
        BlueBird,
        PinkBird,
        VanBird,
        WhiteBird,
        YellowBird,
        GreenBird,
        LittleBlueBird
    }
    public abstract class Bird : MonoBase,IBoom, IDiedAudio,IDied
    {
        public GameObject go => this.gameObject;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        /// <summary>
        /// 小鸟类型
        /// </summary>
        protected EnumBirdType m_BirdType=EnumBirdType.None;
        /// <summary>
        /// 小鸟精灵渲染器
        /// </summary>
        protected SpriteRenderer m_Sr = null;
        /// <summary>
        /// 音效
        /// </summary>
        protected Audio m_Effect = null;
        /// <summary>
        /// 是否碰撞
        /// </summary>
        protected bool m_IsCollision = false;
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
        /// 协程
        /// </summary>
        protected Coroutine m_Cor;
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
            m_ConfigInfo = BirdConfigInfo.GetBirdConfigInfo(m_BirdType);
            m_Sr =GetComponent<SpriteRenderer>();
            m_CC2D =GetComponent<CircleCollider2D>();
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
            m_IsCollision = false;
            transform.eulerAngles = Vector3.forward * 25;
        }      
        protected override void Start()
        {
            base.Start();
            InitTrailRenderer();//初始化拖尾                               
            m_Sr.sortingOrder = m_ConfigInfo.OrderInLayer;//设置自身渲染层级

        }
        protected override void OnDisable()
        {
            base.OnDisable();
            #region RemoveUpdate
            RemoveBirdControlUpdate();
            RemoveOnBirdFlyUpdate_Common();
            #endregion
            RecyclyAudio();//回收音效       
            GameManager.RemoveBird(this);//将小鸟从游戏管理器中移除
            GameManager.RemoveDiedTarget(this);
        }
        protected virtual void OnMouseEnter()
        {
            
        }

        protected virtual void OnMouseDown()
        {
            if (!WindowRoot.Exists)
            {
                return;
            }
            WindowRoot windowRoot = WindowRoot.GetSingleton();
            if (windowRoot.ES.IsPointerOverGameObject())//当操作对象是UI时则屏蔽此次事件响应
            {
                return;
            }
            if (!GameLogic.BirdIsNowComeBirdLogic(this))
                return;
            m_IsCheck = true;
            if (SlingShot.Exists)
            {
                SlingShot.GetSingleton().PlaySlingShotAudio();
            }
            if (FlyPath.Exists)
            {
                FlyPath.GetSingleton().ActiveFlyPath(true);
            }
            PlaySelectAudio();//播放选择音效
        }

        protected virtual void OnMouseUp()
        {
            if (!WindowRoot.Exists)
            {
                return;
            }
            WindowRoot windowRoot = WindowRoot.GetSingleton();
            if (windowRoot.ES.IsPointerOverGameObject())//当操作对象是UI时则屏蔽此次事件响应
            {
                return;
            }
            if ((!m_IsCheck))
            {
                return;
            }
            if (!GameLogic.BirdIsNowComeBirdLogic(this))
                return;
            m_IsCheck = false;
            this.RemoveBirdControlUpdate();
            if (SlingShot.Exists)
            {
                SlingShot slingShot = SlingShot.GetSingleton();
                slingShot.PauseSlingShotAudio();
                slingShot.BreakBird();//断开关联
                slingShot.ClearLine();//清除线
            }
            PlayFlyAudio();//播放飞行音效
            m_Anim.SetTrigger("IsFly");//飞行动画
            ActiveTrailRenderer(true);//开启拖尾
            GameLogic.NowComeBird = null;//断开引用关联
        }

        protected virtual void OnMouseExit()
        {
            
        }
        #region Update
        /// <summary>
        /// 添加小鸟控制更新
        /// </summary>
        public void AddBirdControlUpdate()
        {
            ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard,this.BirdControlUpdate);//添加小鸟控制更新

        }
        /// <summary>
        /// 移除小鸟控制更新
        /// </summary>
        public void RemoveBirdControlUpdate()
        {
            ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,this.BirdControlUpdate);//移除小鸟控制更新
        }
        private void BirdControlUpdate()
        {            
            if (m_IsCheck)//是否选中
            {
                if (Camera2D.Exists)
                {
                    Vector3 movePos = Camera2D.GetSingleton().ScreenToWorldPoint(Input.mousePosition, transform.position.z);
                    if (SlingShot.Exists)
                    {
                        SlingShot slingShot = SlingShot.GetSingleton();
                        Vector3 dir = movePos - slingShot.GetOriginGlobalPos(transform.position.z);                    
                        //限制拉伸的范围
                        transform.position = dir.magnitude <= slingShot.StretchDis ? movePos : dir.normalized * slingShot.StretchDis + slingShot.GetOriginGlobalPos(transform.position.z);
                        //渲染线
                        slingShot.RendererLine(drawLineEnd.position);
                        //计算预瞄准点位置                      
                        if (FlyPath.Exists)
                        {
                            FlyPath.GetSingleton().SetFlyPath(0.2f, 0.2f);
                        }
                        //小鸟面朝发射方向
                        transform.eulerAngles = new Vector3(0, 0, -Vector2.SignedAngle(-dir.normalized, Vector2.right));
                    }             
                }
            }
            else
            {
               
                SlingShot slingShot = SlingShot.GetSingleton();
                //渲染线
                slingShot.RendererLine(drawLineEnd.position);
            }
        }
        
        public void AddOnBirdFlyUpdate_Common()
        {
            ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.OnBirdFlyUpdate_Common);
        }
        public void RemoveOnBirdFlyUpdate_Common()
        {
            ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.OnBirdFlyUpdate_Common);
        }
        /// <summary>
        /// 监听小鸟飞行更新
        /// </summary>
        private void OnBirdFlyUpdate_Common()//用于处理鸟的飞行后的首次碰撞  
        {     
            //小鸟面朝飞行方向
            transform.eulerAngles = new Vector3(0, 0, -Vector2.SignedAngle(m_Rig2D.velocity.normalized, Vector2.right));        
        }
        #endregion

        /// <summary>
        /// 监听小鸟飞行中断
        /// </summary>
        protected virtual void OnBirdFlyBreak()
        {
            m_Anim.SetTrigger("IsHurt");//受伤动画            
            m_Cor = ShareMono.GetSingleton().DelayAction(3f, () =>
            {
                if (gameObject.activeInHierarchy)
                {
                    OpenBoom(); //打开死亡特效
                    PlayDiedAudio();//播放销毁音效               
                    Died();//小鸟死亡
                }
            });
        }

        #region Collision
        /// <summary>
        /// 监听碰撞(替代OnBirdFlyUpdate_Common)
        /// </summary>
        /// <param name="collision"></param>
        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {        
            if (m_IsCollision)
            {
                return;
            }
            RemoveOnBirdFlyUpdate_Common();
            m_IsCollision = true;
            Debuger.Log("技能失效");
            ActiveTrailRenderer(false);//关闭拖尾
            PlayCollisionAudio();//播放碰撞音效
            OnBirdFlyBreak();//触发小鸟飞行中断
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
            PlayAudio(m_ConfigInfo.GetFlyAudioPaths());
        }
        /// <summary>
        /// 播放选中音效
        /// </summary>
        protected virtual void PlaySelectAudio()
        {      
            PlayAudio(m_ConfigInfo.GetSelectAudioPaths());
        }
        /// <summary>
        /// 播放死亡音效
        /// </summary>
        protected virtual void PlayDiedAudio()
        {        
            PlayAudio(m_ConfigInfo.GetDiedAudioPath());
        }
        /// <summary>
        /// 播放碰撞音效
        /// </summary>
        protected virtual void PlayCollisionAudio()
        {
            PlayAudio(m_ConfigInfo.GetCollisionAudioPaths());
        }
        /// <summary>
        /// 播放技能音效
        /// </summary>
        protected virtual void PlaySkillAudio()
        {
            
        }
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="audioPath">音效路径</param>
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
        /// <summary>
        /// 申请音效
        /// </summary>
        protected void ApplyAudio()
        {
            if(m_Effect==null)
            {
                m_Effect = AudioManager.ApplyForAudio();
                m_Effect.SpatialBlend = 0;//设置为2D
                m_Effect.AbleRecycle = false;//不可自动回收
                m_Effect.Group = AudioMixerManager.GetAudioMixerGroup("Effect");
            }
        }     
        /// <summary>
        /// 回收音效
        /// </summary>
        protected void RecyclyAudio()
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
            Boom.OpenBoom(EnumBoomType.BirdBoom,transform.position);
        }
        #endregion

        #region DiedAudio
        public virtual void DiedAudio()
        {
            PlayDiedAudio();
        }
        #endregion

        #region Died
        public virtual void Died(bool isDestroy=false)
        {
            CancleCor();
            if (isDestroy)
            {
                DestroyImmediate(gameObject);
            }
            else 
            {
                Debuger.Log("回收鸟");
                GoReusePool.Put(m_BirdType.ToString(), this.gameObject);//回收小鸟
            }
        }
        #endregion
        /// <summary>
        /// 撤销协程的监听
        /// </summary>
        protected void CancleCor()
        {
            if (m_Cor != null)
            {
                ShareMono.GetSingleton().StopCoroutine(m_Cor);//撤销游戏结束后小鸟延迟销毁逻辑的执行
                m_Cor = null;
            }
        }
    }
}
