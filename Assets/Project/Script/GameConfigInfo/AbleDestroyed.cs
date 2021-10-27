using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 可销毁
    /// </summary>
    public abstract class AbleDestroyed : BaseConfig
    {
        protected AbleDestroyed() { }

        public override bool InitResources()
        {
            if(base.InitResources())
            {
                m_BoomPath = "Common/Boom";
                return true;
            }
            return false;
        }
        
    }
}
