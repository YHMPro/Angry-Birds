using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using DG.Tweening;
using Farme.UI;
using Farme.Tool;
namespace Bird_VS_Boar
{
    public class Egg : MonoBehaviour,IDied
    {
        public GameObject go => this.gameObject;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        /// <summary>
        /// 是否可选择
        /// </summary>
        private bool m_IsSelectable = false;
        /// <summary>
        /// 是否碰撞
        /// </summary>
        private bool m_IsCollision = false;
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

        private void OnEnable()
        {
            GameManager.AddDiedTarget(this);
        }

       
        private void OnMouseDown()
        {
            if (m_IsSelectable)
            {
                m_IsSelectable = false;              
                GameLogic.CoinAdd();
                Died(true);//回收     
            }
        }
        private void OnDisable()
        {
            GameManager.RemoveDiedTarget(this);
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!m_IsCollision)
            {
                m_IsSelectable = true;
            }
        }

        #region Velocity
        /// <summary>
        /// 设置鸡蛋飞行速度
        /// </summary>
        /// <param name="velocity">速度</param>
        public virtual void SetBirdFlyVelocity(Vector2 velocity)
        {
            m_Rig2D.velocity = velocity;
        }
        #endregion
        
        #region Died
        public void Died(bool isDestroy = false)
        {
            if(isDestroy)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                GoReusePool.Put(typeof(Egg).Name, gameObject);
            }
        }
        #endregion
    }
}
