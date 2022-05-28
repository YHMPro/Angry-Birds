using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
namespace Bird_VS_Boar
{
    /// <summary>
    /// 冲击波
    /// </summary>
    public class Shockware : MonoBehaviour
    {
        [SerializeField]
        private float m_ShockwareSize = 3;
        [SerializeField]
        private float m_ExpandSize = 15f;
        private Rigidbody2D m_Rig2D;
        private ContactFilter2D m_CFilter;
        private ContactPoint2D[] m_CPoints;
        private CircleCollider2D m_Co;
        private float m_RadiusMax = 1.5f;
        private void Awake()
        {
            m_Rig2D=GetComponent<Rigidbody2D>();
            m_Co =GetComponent<CircleCollider2D>();
            m_CPoints = new ContactPoint2D[10];
            m_CFilter.SetLayerMask(LayerMask.GetMask(new string[]{ "Barrier","Pig" }));
        }

        private void Start()
        {

        }
        private void OnEnable()
        {
            ShareMono.GetSingleton().ApplyUpdateAction(EnumUpdateAction.Standard, this.Expand);
        }
       
        private void OnDisable()
        {
            if (ShareMono.Exists)
            {
                ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.Expand);
            }
            m_Co.radius = 0.001f;
        }

        private void Expand()
        {
            ApplyForce();
            float radius = m_Co.radius;
            radius += Time.deltaTime* m_ExpandSize;
            m_Co.radius = Mathf.Clamp(radius, 0.001f, m_RadiusMax);
            if(m_Co.radius== m_RadiusMax)
            {
                ShareMono.GetSingleton().RemoveUpdateAction(EnumUpdateAction.Standard, this.Expand);
                this.gameObject.SetActive(false);
            }
           
        }

        private void ApplyForce()
        {
            int length = m_Rig2D.GetContacts(m_CFilter, m_CPoints);
            Vector2 dir;
            for (int i = 0; i < length; i++)
            {
                Rigidbody2D rig2D = m_CPoints[i].rigidbody;
                dir = (m_CPoints[i].point - (Vector2)transform.position).normalized;
                rig2D.AddForceAtPosition(m_ShockwareSize * dir, m_CPoints[i].point, ForceMode2D.Impulse);
            }
        }
    }
}
