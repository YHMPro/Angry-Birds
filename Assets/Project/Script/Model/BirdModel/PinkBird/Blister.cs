using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Angry_Birds
{
    public class Blister : MonoBehaviour
    {
        private EdgeCollider2D m_EdgeCo2D = null;//碰撞器
        private EdgeCollider2D m_EdgeTr2D = null;//触发器
        private static List<GameObject> m_CheckGo = null;
        private GameObject m_ControlGo = null;
        [SerializeField]
        private float m_UpMoveSpeed = 10f;
        private Vector3 m_MoveDir = new Vector3(0.5f, 1,0);
        private void Awake()
        {
            if(m_CheckGo==null)
            {
                m_CheckGo = new List<GameObject>();
            }
            m_EdgeTr2D = GetComponent<EdgeCollider2D>();
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
            if (m_ControlGo == null)
            {
                if (!m_CheckGo.Contains(collision.gameObject))
                {
                    m_ControlGo = collision.gameObject;
                    m_CheckGo.Add(collision.gameObject);
                    transform.position = m_ControlGo.transform.position;
                    m_ControlGo.GetComponent<Pig>().SetGravityScale(0.01f);

                    if (m_EdgeCo2D==null)
                    {
                        m_EdgeCo2D = gameObject.AddComponent<EdgeCollider2D>();
                        m_EdgeCo2D.points = m_EdgeTr2D.points;
                    }
                    else
                    {
                        m_EdgeCo2D.enabled = true;
                    }
                    MonoSingletonFactory<ShareMono>.GetSingleton().AddUpdateUAction(FlyUpdate);
                    MonoSingletonFactory<ShareMono>.GetSingleton().DelayUAction(10f, () =>
                    {
                        MonoSingletonFactory<ShareMono>.GetSingleton().RemoveUpdateUAction(FlyUpdate);
                        if(m_ControlGo!=null)
                        {
                            m_ControlGo.GetComponent<Pig>().SetGravityScale(1.0f);
                        }
                        m_CheckGo.Add(m_ControlGo);
                        Destroy(gameObject);
                    });
                }
            }
        }

    }
}
