using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
using Farme.Tool;
namespace Bird_VS_Boar
{
    public class Blister : MonoBehaviour,IDied
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
        private Coroutine m_Cor = null;
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
            CheckGoLi.Clear();
            m_CheckGo = null;
            m_EdgeCo2D.enabled = false;
            GameManager.AddDiedTarget(this);
            m_Cor = ShareMono.GetSingleton().DelayAction(3.0f, Recycle);
        }
        private void Start()
        {
           
        }

        private void OnDisable()
        {
            ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.FlyUpdate);
            GameManager.RemoveDiedTarget(this);     
            CancleCor();      
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
                    CancleCor();
                     m_CheckGo = collision.gameObject;
                    if (!CheckGoLi.Contains(collision.gameObject))
                    {
                        CheckGoLi.Add(collision.gameObject);
                    }
                    transform.position = m_CheckGo.transform.position;
                    m_CheckGo.GetComponent<Pig>().SetGravityScale(0.01f);
                    m_EdgeCo2D.enabled = true;
                    ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard,this.FlyUpdate);
                    m_Cor= ShareMono.GetSingleton().DelayAction(10f, () =>
                    {
                        ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard,this.FlyUpdate);
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
                DestroyImmediate(gameObject);
            }
            else
            {
                Recycle();
            }
        }
        #endregion
        /// <summary>
        /// 撤销协程监听
        /// </summary>
        private void CancleCor()
        {
            if (m_Cor != null)
            {
                ShareMono.GetSingleton().StopCoroutine(m_Cor);
                m_Cor = null;
            }
        }
    }
}
