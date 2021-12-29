using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Audio;
using Farme.Tool;

namespace Bird_VS_Boar
{
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
    public abstract class Pig : BaseMono, IBoom, IScore
    {
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
            m_Rig2D = GetComponent<Rigidbody2D>();
            m_Anim = GetComponent<Animator>();
        }

        protected override void Start()
        {
            base.Start();
        }
        protected override void OnDestroy()
        {
            RecyclyAudio();
            base.OnDestroy();          
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            float relativeSpeed = collision.relativeVelocity.magnitude;
            if (relativeSpeed <5)
                return;
            m_HurtGrade = relativeSpeed <= 10 ? EnumPigHurtGrade.Hurt1 :
                relativeSpeed <= 15 ? EnumPigHurtGrade.Hurt2 : EnumPigHurtGrade.Destroy;
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
                Destroy(gameObject);//回收猪 待
            }
        }

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
            if (!PigConfigInfo.PigConfigInfoDic.TryGetValue(GetType().Name, out var config))
            {
                return;
            }
            PlayAudio(config.GetDiedAudioPath());
        }
        protected virtual void PlayHurtAudio()
        {
            if (!PigConfigInfo.PigConfigInfoDic.TryGetValue(GetType().Name, out var config))
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
    }
}
