using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 弹弓配置
    /// </summary>
    public class SlingShotConfig : BaseConfig
    {
        #region Resources资源       
        /// <summary>
        /// 弹弓音效
        /// </summary>
        protected string m_SlingShotAudioPath = null;
        /// <summary>
        /// 弹弓音效
        /// </summary>
        public string SlingShotAudioPath
        {
            get
            {
                return m_SlingShotAudioPath;
            }
        }
        #endregion

        public override bool InitResourcesPath()
        {
            
            if (base.InitResourcesPath())
            {
                m_SlingShotAudioPath=  m_Tag + "/SlingShotAudio1" ;
                m_IsInit = true;
                return true;
            }
            return false;          
        }

        public override bool InitABPath()
        {          
            if (base.InitABPath())
            {
                m_SlingShotAudioPath = "SlingShotAudio1";
                m_IsInit = true;
                return true;
            }
            return false;
        }       
    }
}
