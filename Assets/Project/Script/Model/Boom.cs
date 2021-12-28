using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public enum ENUM_BoomType
    {
        PigBoom,
        BirdBoom
    }
    public class Boom : MonoBehaviour
    {
        private Animator m_Anim;
        private void Awake()
        {
            m_Anim = GetComponent<Animator>();
        }
        public void OpenBoom(ENUM_BoomType boomType)
        {
            m_Anim.SetTrigger(boomType.ToString());
        }
        [SerializeField]
        private void CloseBoom()
        {
            GoReusePool.Put(GetType().Name, gameObject);                          
        }
    }
}
