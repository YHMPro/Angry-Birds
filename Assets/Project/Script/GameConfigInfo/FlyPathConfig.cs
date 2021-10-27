using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class FlyPathConfig : BaseConfig
    {
        public override bool InitResources()
        {
            if(base.InitResources())
            {
                m_IsInit = true;
                m_PointPath = m_Tag + "/Point";
                return true; 
            }
            return false;
        }
    }
}
