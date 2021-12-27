using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    public class Egg : MonoBehaviour
    {
        /// <summary>
        /// 是否可选择
        /// </summary>
        private bool m_IsSelectable = false;

        /// <summary>
        /// 碰撞盒子
        /// </summary>
        private CircleCollider2D m_CC2D = null;
        /// <summary>
        /// 刚体
        /// </summary>
        private Rigidbody2D m_Rig2D = null;
        /// <summary>
        /// 射线检测的组
        /// </summary>
        protected string[] rayCastGroup = new string[] {"Land" };
        private void Awake()
        {
            m_CC2D = GetComponent<CircleCollider2D>();
            m_Rig2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard,EggUpdate);
        }
        private void OnMouseDown()
        {
           if(m_IsSelectable)
            {
                m_IsSelectable = false;
                Debug.Log("收集一刻蛋");//设置一个目标点做缓动运动      
            }
        }
        private void EggUpdate()
        {
            //if (Physics2D.OverlapCircle(transform.position, m_CC2D.radius, LayerMask.GetMask(rayCastGroup)))
            //{
            //    MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(EggUpdate);
            //    m_Rig2D.isKinematic = true;
            //    m_Rig2D.velocity *= 0;
            //    m_Rig2D.angularVelocity *= 0;
            //    m_CC2D.isTrigger = true;
            //    m_IsSelectable = true;
            //}
        }
    }
}
