using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 翅膀
    /// </summary>
    public class Wing : BaseMono
    {
        private Animator m_Anim;
        protected override void Awake()
        {
            base.Awake();
            m_Anim = GetComponent<Animator>();


        }





    }
}
