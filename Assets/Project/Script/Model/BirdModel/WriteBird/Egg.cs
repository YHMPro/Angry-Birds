using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using DG.Tweening;
using Farme.UI;
using Farme.Tool;
namespace Bird_VS_Boar
{
    public class Egg : MonoBehaviour,IDied,IWeaken
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
        /// 刚体
        /// </summary>
        private Rigidbody2D m_Rig2D = null;
        /// <summary>
        /// 速度
        /// </summary>
        public Vector2 Velocity
        {
            set
            {
                m_Rig2D.velocity = value;
            }
            get
            {
                return m_Rig2D.velocity;
            }
        }
        /// <summary>
        /// 射线检测的组
        /// </summary>
        protected string[] rayCastGroup = new string[] {"Land" };
        private void Awake()
        {
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
