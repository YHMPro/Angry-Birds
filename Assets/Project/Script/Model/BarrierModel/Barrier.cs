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
    /// 障碍物破裂等级
    /// </summary>
    public enum EnumBarrierBrokenGrade
    {
        None,
        Broken1,
        Broken2,
        Broken3,
        Broken4,
        Destroy
    }
    /// <summary>
    /// 障碍物类型
    /// </summary>
    public enum EnumBarrierType
    { 
        None,
        Ice,
        Rock,
        Wood
    }
    /// <summary>
    /// 障碍物形状类型
    /// </summary>
    public enum EnumBarrierShapeType
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 长长方形
        /// </summary>
        Long_Rectangle,
        /// <summary>
        /// 短长方形
        /// </summary>
        Short_Rectangle,
        /// <summary>
        /// 实心三角形
        /// </summary>
        SolidTriangle,
        /// <summary>
        /// 空心三角形
        /// </summary>
        HollowTriangle,
        /// <summary>
        /// 实心正方形
        /// </summary>
        SolidSquare,
        /// <summary>
        /// 空心正方形
        /// </summary>
        HollowSquare,
        /// <summary>
        /// 圆形
        /// </summary>
        Circle,

    }
    [Serializable]
    public abstract class Barrier : BaseMono, IScore, IDiedAudio, IDied
    {
        public GameObject go => this.gameObject;
        /// <summary>
        /// 配置信息
        /// </summary>
        protected BarrierConfigInfo m_ConfigInfo;
        /// <summary>
        /// 碰撞器
        /// </summary>
        protected Collider2D m_Co = null;
        [SerializeField]
        /// <summary>
        /// 障碍物形状
        /// </summary>
        protected EnumBarrierShapeType m_BarrierShapeType;
        /// <summary>
        /// 承受的能量总和(达到一定量后会出现破裂)
        /// </summary>
        protected float m_BearEnergySum = 0;
        [SerializeField]
        /// <summary>
        /// 障碍物类型
        /// </summary>
        protected EnumBarrierType m_BarrierType = EnumBarrierType.None;
        /// <summary>
        /// 精灵
        /// </summary>
        protected SpriteRenderer m_Sr = null;
        /// <summary>
        /// 音效
        /// </summary>
        protected Audio m_Effect = null;
        [SerializeField]
        /// <summary>
        /// 精灵数组
        /// </summary>
        private Sprite[] m_Sps = null;
        /// <summary>
        /// 刚体
        /// </summary>
        protected Rigidbody2D m_Rig2D = null;
        /// <summary>
        /// 受伤等级
        /// </summary>
        protected EnumBarrierBrokenGrade m_HurtGrade = EnumBarrierBrokenGrade.None;
        [SerializeField]
        /// <summary>
        /// 分数类型
        /// </summary>
        protected EnumScoreType m_ScoreType = EnumScoreType.None;
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
        /// 是否碰撞
        /// </summary>
        protected bool m_IsCollision = false;
        protected override void Awake()
        {
            base.Awake();
            m_ConfigInfo = BarrierConfigInfo.GetBarrierConfigInfo(m_BarrierType);
            m_Rig2D = GetComponent<Rigidbody2D>();
            m_Sr=GetComponent<SpriteRenderer>();
            m_Co=GetComponent<Collider2D>();
            m_Co.enabled = false;
        }

        protected override void Start()
        {
            base.Start();
            //设置自身渲染层级
            m_Sr.sortingOrder = m_ConfigInfo.OrderInLayer;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            m_IsCollision = false;
            m_Rig2D.isKinematic = true;
            GameManager.AddDiedTarget(this);
            m_BearEnergySum = 0;
            m_Sr.sprite = m_Sps[0];
            m_HurtGrade = EnumBarrierBrokenGrade.None;
        }
        protected override void LateOnEnable()
        {
            base.LateOnEnable();
            m_Rig2D.isKinematic = false;
            m_Co.enabled = true;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GameManager.RemoveDiedTarget(this);
            RecyclyAudio();//回收音效
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(m_IsCollision)
            {
                return;
            }
            float relativeSpeed = collision.relativeVelocity.magnitude;
            if (relativeSpeed <= 6)
            {
                if(relativeSpeed>2)
                {
                    //播放破裂声音
                    PlayBrokenAudio();
                }
                return;
            }
            m_BearEnergySum += relativeSpeed;
            m_HurtGrade = m_BearEnergySum <=12 ? EnumBarrierBrokenGrade.Broken1 :
                m_BearEnergySum <= 18 ? EnumBarrierBrokenGrade.Broken2 : EnumBarrierBrokenGrade.Destroy;
            if (m_HurtGrade != EnumBarrierBrokenGrade.Destroy)
            {
                PlayBrokenAudio();
                SetBrokenGradeSprite();
            }
            else
            {
                m_IsCollision = true;
                OpenScore();//打开分数
                OpenBoom();//打开Boom特效
                PlayDiedAudio();//播放死亡音效
                Died();//死亡
            }
        }

        #region Sprite
        /// <summary>
        /// 设置破碎等级精灵
        /// </summary>
        protected virtual void SetBrokenGradeSprite()
        {
            m_Sr.sprite = m_Sps[Mathf.Clamp((int)m_HurtGrade,1, m_Sps.Length-1)];
        }
        #endregion

        #region Audio
        protected virtual void PlayDiedAudio()
        {          
            PlayAudio(m_ConfigInfo.GetBarrierDestroyAudioPath());
        }
        protected virtual void PlayBrokenAudio()
        {       
            PlayAudio(m_ConfigInfo.GetBarrierBrokenAudioPath(Mathf.Clamp((int)m_HurtGrade, 1, m_Sps.Length - 1)));
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
            Boom.OpenBoom(EnumBoomType.None,transform.position);                  
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

        #region Died
        public virtual void Died(bool isDestroy=false)
        {
            if (isDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                Debuger.Log("回收障碍物");
                m_Co.enabled = false;
                GoReusePool.Put(m_BarrierType.ToString() + m_BarrierShapeType.ToString(), this.gameObject);//回收该障碍物
            }
        }
        #endregion

        #region BarrierConfig
        /// <summary>
        /// 获取障碍物的配置
        /// </summary>
        public BarrierConfig GetBarrierConfig()
        {
            BarrierConfig barrierConfig = new BarrierConfig();
            barrierConfig.Euler.SetValue(transform.eulerAngles);
            barrierConfig.Scale.SetValue(transform.lossyScale);
            barrierConfig.Position.SetValue(transform.position);
            barrierConfig.BarrierType = m_BarrierType;
            barrierConfig.BarrierShapeType = m_BarrierShapeType;
            return barrierConfig;
        }
        #endregion

       
    }
}
