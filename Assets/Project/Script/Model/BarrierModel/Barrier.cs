﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Audio;
using Farme.Tool;

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
    public class Barrier : BaseMono, IScore,IDiedAudio
    {
        /// <summary>
        /// 障碍物形状
        /// </summary>
        public EnumBarrierShapeType m_BarrierShapeType;
        /// <summary>
        /// 承受的能量总和(达到一定量后会出现破裂)
        /// </summary>
        protected float m_BearEnergySum = 0;
        /// <summary>
        /// 障碍物类型
        /// </summary>
        protected EnumBarrierType m_BarrierType= EnumBarrierType.None;
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
        protected override void Awake()
        {
            base.Awake();
            m_Rig2D = GetComponent<Rigidbody2D>();
            m_Sr=GetComponent<SpriteRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            if (BarrierConfigInfo.BarrierConfigInfoDic.TryGetValue(m_BarrierType, out var config))
            {
                //设置自身渲染层级
                m_Sr.sortingOrder = config.OrderInLayer;
            }
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            m_Sr.sprite = m_Sps[0];
        }

        protected override void OnDestroy()
        {
            RecyclyAudio();//回收音效
            base.OnDestroy();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
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
                OpenScore();//打开分数
                OpenBoom();//打开Boom特效
                PlayDiedAudio();//播放死亡音效
                Destroy(gameObject);//回收猪 待
            }
        }

        #region Animator
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
            if (!BarrierConfigInfo.BarrierConfigInfoDic.TryGetValue(m_BarrierType, out var config))
            {
                return;
            }
            PlayAudio(config.GetBarrierDestroyAudioPath());
        }
        protected virtual void PlayBrokenAudio()
        {
            if (!BarrierConfigInfo.BarrierConfigInfoDic.TryGetValue(m_BarrierType, out var config))
            {
                return;
            }
            PlayAudio(config.GetBarrierBrokenAudioPath(Mathf.Clamp((int)m_HurtGrade, 1, m_Sps.Length - 1)));
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
    }
}
