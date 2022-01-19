using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Audio;
using Farme.Tool;
using Bird_VS_Boar.LevelConfig;
using System;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 猪的类型
    /// </summary>
    public enum EnumPigType
    {
        None,
        OldPig,
        RockPig,
        YoungPig
    }
    /// <summary>
    /// 猪的受伤等级
    /// </summary>
    public enum EnumPigHurtGrade
    {
        None,
        Hurt1,
        Hurt2,
        Hurt3,
        Hurt4,
        Destroy
    }
    [Serializable]
    public abstract class Pig : BaseMono, IBoom, IScore,IDiedAudio,IDied
    {
        /// <summary>
        /// 精灵渲染器
        /// </summary>
        protected SpriteRenderer m_Sr = null;
        /// <summary>
        /// 承受的能量总和(达到一定量后会出现受伤)
        /// </summary>
        protected float m_BearEnergySum = 0;
        [SerializeField]
        /// <summary>
        /// 猪的类型
        /// </summary>
        protected EnumPigType m_PigType=EnumPigType.None;
        /// <summary>
        /// 音效
        /// </summary>
        protected Audio m_Effect = null;
        /// <summary>
        /// 刚体
        /// </summary>
        protected Rigidbody2D m_Rig2D = null;
        /// <summary>
        /// 动画控制器
        /// </summary>
        protected Animator m_Anim = null;
        /// <summary>
        /// 受伤等级
        /// </summary>
        protected EnumPigHurtGrade m_HurtGrade = EnumPigHurtGrade.None;
        [SerializeField]
        /// <summary>
        /// 分数类型
        /// </summary>
        protected EnumScoreType m_ScoreType=EnumScoreType.None;
        protected override void Awake()
        {
            base.Awake();
            m_Sr=GetComponent<SpriteRenderer>();
            m_Rig2D = GetComponent<Rigidbody2D>();
            m_Anim = GetComponent<Animator>();
        }

        protected override void Start()
        {
            base.Start();
            if (PigConfigInfo.PigConfigInfoDic.TryGetValue(m_PigType, out var config))
            {
                //设置自身渲染层级
                m_Sr.sortingOrder = config.OrderInLayer;
            }                  
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_Rig2D.isKinematic = true;           
            GameManager.AddPig(this);//添加到游戏管理器中
            GameManager.AddDiedTarget(this);
            m_HurtGrade = EnumPigHurtGrade.None;
            SetHurtGradeAnim();
        }

        protected override void LateOnEnable()
        {
            base.LateOnEnable();
            m_Rig2D.isKinematic = false;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GameManager.RemovePig(this);//从游戏管理器中移除
            GameManager.RemoveDiedTarget(this);
            RecyclyAudio();
        }      
        #region Collision
        private void OnCollisionEnter2D(Collision2D collision)
        {
            float relativeSpeed = collision.relativeVelocity.magnitude;
            if (relativeSpeed < 5)
            {
                if (relativeSpeed > 2)
                {
                    //播放受伤声音
                    PlayHurtAudio();
                }
                return;
            }
            m_BearEnergySum += relativeSpeed;
            m_HurtGrade = m_BearEnergySum <= 10 ? EnumPigHurtGrade.Hurt1 :
                m_BearEnergySum <= 15 ? EnumPigHurtGrade.Hurt2 : EnumPigHurtGrade.Destroy;
            if(m_HurtGrade!= EnumPigHurtGrade.Destroy)
            {
                PlayHurtAudio();
                SetHurtGradeAnim();         
            }
            else
            {
                OpenScore();//打开分数
                OpenBoom();//打开Boom特效
                PlayDiedAudio();//播放死亡音效                
                Died();//死亡
            }
        }
        #endregion

        #region Animator
        /// <summary>
        /// 设置受伤等级动画
        /// </summary>
        protected virtual void SetHurtGradeAnim()
        {
            m_Anim.SetTrigger(m_HurtGrade.ToString());
        }
        #endregion

        #region Audio
        protected virtual void PlayDiedAudio()
        {
            if (!PigConfigInfo.PigConfigInfoDic.TryGetValue(m_PigType, out var config))
            {
                return;
            }
            PlayAudio(config.GetDiedAudioPath());
        }
        protected virtual void PlayHurtAudio()
        {
            if (!PigConfigInfo.PigConfigInfoDic.TryGetValue(m_PigType, out var config))
            {
                return;
            }
            PlayAudio(config.GetHurtAudioPath());
        }
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
            if (AudioClipManager.GetAudioClip(audioPath, out AudioClip clip))
            {
                m_Effect.Clip = clip;
                m_Effect.Play();
            }
        }
        private void ApplyAudio()
        {
            if (m_Effect == null)
            {
                m_Effect = AudioManager.ApplyForAudio();
                m_Effect.SpatialBlend = 0;//设置为2D
                m_Effect.AbleRecycle = false;//不可自动回收
                m_Effect.Group = AudioMixerManager.GetAudioMixerGroup("Effect");
            }
        }
        private void RecyclyAudio()
        {
            if (m_Effect != null)
            {
                m_Effect.AbleRecycle = true;
                m_Effect = null;
            }
        }
        #endregion

        #region Rig
        /// <summary>
        /// 设置重力
        /// </summary>
        /// <param name="value"></param>
        public void SetGravityScale(float value)
        {
            m_Rig2D.gravityScale = value;
        }
        #endregion

        #region Boom
        public virtual void OpenBoom()
        {      
            Boom.OpenBoom(EnumBoomType.PigBoom,transform.position);
        }
        #endregion

        #region Score
        public virtual void OpenScore()
        {          
            Score.OpenScore(m_ScoreType,transform.position);
        }
        #endregion

        #region DestroyAudio
        public virtual void DiedAudio()
        {
            PlayDiedAudio();
        }
        #endregion

        #region PigDied
        public virtual void Died()
        {           
            GoReusePool.Put(m_PigType.ToString(), this.gameObject);//回收该猪
        }
        #endregion

        #region PigConfig
        /// <summary>
        /// 获取猪的配置
        /// </summary>
        public PigConfig GetPigConfig()
        {
            PigConfig pigConfig = new PigConfig();
            pigConfig.Euler.SetValue(transform.eulerAngles);
            pigConfig.Scale.SetValue(transform.lossyScale);
            pigConfig.Position.SetValue(transform.position);
            pigConfig.PigType = m_PigType;
            return pigConfig;
        }
        #endregion
    }
}
