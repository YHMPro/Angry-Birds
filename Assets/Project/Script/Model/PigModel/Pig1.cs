using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class Pig1 : Pig
    {
        protected override void Awake()
        {
            m_ConfigInfo = NotMonoSingletonFactory<Pig1ConfigInfo>.GetSingleton();
            base.Awake();
        }
    }
}
