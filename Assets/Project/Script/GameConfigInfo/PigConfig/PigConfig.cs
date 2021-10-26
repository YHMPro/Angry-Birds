using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 猪配置
    /// </summary>
    public abstract class PigConfig : AbleCollision
    {
        protected PigConfig() { }
        public override bool InitResources()
        {
            if (base.InitResources())
            {
                m_Common = "PigCommon";
                m_DestroyAudioPaths = new string[] { m_Common + "/PDe1" };
                return true;
            }
            return false;
        }
    }


}
