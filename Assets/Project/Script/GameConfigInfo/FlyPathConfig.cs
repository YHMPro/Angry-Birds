using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class FlyPathConfig : BaseConfig
    {
        /// <summary>
        /// 点路径
        /// </summary>
        private string m_PointPath = "";
        /// <summary>
        /// 点路径
        /// </summary>
        public string PointPath
        {
            get
            {
                return m_PointPath;
            }
        }

        public override bool InitResources()
        {
            if(base.InitResources())
            {
                m_PointPath = m_Tag + "/Point";
                return true; 
            }
            return false;
        }
    }
}
