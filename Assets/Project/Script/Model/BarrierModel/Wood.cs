using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class Wood : Barrier
    {
        protected override void Awake()
        {
            m_BarrierType = EnumBarrierType.Wood;
            base.Awake();
        }


    }
}
