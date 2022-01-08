using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bird_VS_Boar
{
    public class RockPig : Pig
    {
        protected override void Awake()
        {
            m_PigType = EnumPigType.RockPig;
            base.Awake();
        }
    }
}
