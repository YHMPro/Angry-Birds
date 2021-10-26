﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class Boom : MonoBehaviour
    {
        private Animator m_Anim;
        private void Awake()
        {
            m_Anim = GetComponent<Animator>();
        }
        public void OpenBoom(string boomName)
        {
            m_Anim.SetTrigger(boomName);
        }

        public void CloseBoom()
        {
            GoReusePool.Put(GetType().Name, gameObject);                          
        }
    }
}
