using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class RedBird : Bird
    {
        
        protected override void Awake()
        {
            m_ConfigInfo = NotMonoSingletonFactory<RedBirdConfigInfo>.GetSingleton();
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();       
        }
    }
}
