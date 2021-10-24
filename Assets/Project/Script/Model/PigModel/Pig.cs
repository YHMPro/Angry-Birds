using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public abstract class Pig : BaseMono
    {
        protected Rigidbody2D m_Rig2D = null;
        protected PigConfigInfo m_ConfigInfo = null;


        protected override void Awake()
        {
            base.Awake();
            m_Rig2D = GetComponent<Rigidbody2D>();
        }

        protected override void OnDestroy()
        {          
            base.OnDestroy();          
        }
        #region Audio
        protected virtual void PlayDiedAudio()
        {
            GameAudio.PlayEffectAudio(m_ConfigInfo.GetDiedAudioPath(true));
        }
        protected virtual void PlayCrashAudio()
        {
            GameAudio.PlayEffectAudio(m_ConfigInfo.GetCrashAudioPath(true));
        }
        protected virtual void PlaySkillAudio()
        {
            GameAudio.PlayEffectAudio(m_ConfigInfo.GetSkillAudioPath(true));
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
    }
}
