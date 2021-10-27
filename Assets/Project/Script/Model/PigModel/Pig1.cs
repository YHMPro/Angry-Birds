using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class Pig1 : Pig
    {
        protected override void Awake()
        {
            m_Config= NotMonoSingletonFactory<Pig1Config>.GetSingleton();
            base.Awake();
        }
    }
}
