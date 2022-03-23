using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class RedBird : Bird
    {

        protected override void Awake()
        {
            m_BirdType = EnumBirdType.RedBird;
            //m_ConfigInfo = BirdConfigInfo.GetBirdConfigInfo<RedBirdConfigInfo>(m_BirdType);
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }
    }
}
