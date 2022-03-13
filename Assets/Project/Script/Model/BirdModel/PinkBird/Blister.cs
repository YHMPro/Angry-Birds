using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
namespace Bird_VS_Boar
{
    public class Blister : MonoBehaviour,IDied
    {
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
        private Coroutine m_C = null;
        /// <summary>
        /// 碰撞器
        /// </summary>
        private EdgeCollider2D m_EdgeCo2D = null;
        /// <summary>
        /// 检测的列表
        /// </summary>
        private static List<GameObject> m_CheckGoLi = null;
        /// <summary>
        /// 检测的列表
        /// </summary>
        private static List<GameObject> CheckGoLi
        {
            get
            {
                if(m_CheckGoLi == null)
                {
                    m_CheckGoLi = new List<GameObject>();
                }
                return m_CheckGoLi;
            }
        }
        /// <summary>
        /// 检测的对象
        /// </summary>
        private GameObject m_CheckGo = null;
        [SerializeField]
        private float m_UpMoveSpeed = 10f;
        private Vector3 m_MoveDir = new Vector3(0, 1,0);
        private void Awake()
        {            
            m_EdgeCo2D = GetComponents<EdgeCollider2D>()[1];
        }

        private void OnEnable()
        {
            m_CheckGo = null;
            m_EdgeCo2D.enabled = false;
            m_C=MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(3.0f, Recycle);
        }
        private void Start()
        {
           
        }
        private void FlyUpdate()
        {                   
            transform.Translate(m_MoveDir * Time.deltaTime* Mathf.MoveTowards(0, m_UpMoveSpeed,0.5f));         
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_CheckGo == null)
            {
                if (!CheckGoLi.Contains(collision.gameObject))
                {
                    if (m_C != null)
                    {
                        MonoSingletonFactory<ShareMono>.GetSingleton().StopCoroutine(m_C);
                    }
                    m_CheckGo = collision.gameObject;
                    if (!CheckGoLi.Contains(collision.gameObject))
                    {
                        CheckGoLi.Add(collision.gameObject);
                    }
                    transform.position = m_CheckGo.transform.position;
                    m_CheckGo.GetComponent<Pig>().SetGravityScale(0.01f);
                    m_EdgeCo2D.enabled = true;
                    MonoSingletonFactory<ShareMono>.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard,FlyUpdate);
                    MonoSingletonFactory<ShareMono>.GetSingleton().DelayAction(10f, () =>
                    {
                        MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,FlyUpdate);
                        if(m_CheckGo != null)
                        {
                            m_CheckGo.GetComponent<Pig>().SetGravityScale(1.0f);
                        }
                        CheckGoLi.Remove(m_CheckGo);
                        Recycle();
                    });
                }
            }
        }

        private void Recycle()
        {
            Debuger.Log("回收气球");
            GoReusePool.Put(GetType().Name, gameObject);
        }
        #region Died
        public void Died(bool isDestroy = false)
        {
            if(isDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                Recycle();
            }
        }
        #endregion
    }
}
