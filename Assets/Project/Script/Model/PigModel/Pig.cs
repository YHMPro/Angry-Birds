using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
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
    public abstract class Pig : BaseMono
    {
        protected PigConfig m_Config = null;
        protected Rigidbody2D m_Rig2D = null;
        protected Animator m_Anim = null;
        protected EnumPigHurtGrade m_HurtGrade = EnumPigHurtGrade.None;
        protected override void Awake()
        {
            base.Awake();
            m_Rig2D = GetComponent<Rigidbody2D>();
            m_Anim = GetComponent<Animator>();
        }

        protected override void Start()
        {
            base.Start();
            m_Config.InitResources();//初始化资源路径
        }
        protected override void OnDestroy()
        {          
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
                PlayCollisionAudio();
                SetHurtGradeAnim();         
            }
            else
            {
                OpenBoom();
                PlayDiedAudio();
                Destroy(gameObject);
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
            GameAudio.PlayEffectAudio(m_Config.GetDestroyedAudioPath());
        }
        protected virtual void PlayCollisionAudio()
        {
            GameAudio.PlayEffectAudio(m_Config.GetCollisionAudioPath());
        }
        protected virtual void PlaySkillAudio()
        {

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
            GameObject go;
            if (!GoReusePool.Take(typeof(Boom).Name, out go))
            {
                if (!GoLoad.Take(m_Config.BoomPath, out go))
                {                  
                    return;
                }
            }
            go.transform.position = transform.position;
            go.GetComponent<Boom>().OpenBoom("PigBoom");
        }
        #endregion
    }
}
