using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ZD : MonoBehaviour
{
    private Rigidbody2D m_Rig2D;
    private CircleCollider2D m_Co;
    private float m_ShockwareSize = 10;
    private ContactFilter2D m_CFilter;
    private ContactPoint2D[] m_CPoints;
    private void Awake()
    {
        m_Rig2D=GetComponent<Rigidbody2D>();
        m_Co=GetComponent<CircleCollider2D>();
        m_CFilter.SetLayerMask(LayerMask.GetMask("Barrier"));
        m_CPoints = new ContactPoint2D[10];
    }
    public void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Co.radius = 1.5f;
            Invoke("Deiley", 0.1f);
        }     
    }
    private void Deiley()
    {
        m_Co.isTrigger = false;
        int length = m_Rig2D.GetContacts(m_CFilter, m_CPoints);
        m_Co.isTrigger = true;
        Vector2 dir;
        for (int i = 0; i < length; i++)
        {
            Rigidbody2D rig2D = m_CPoints[i].rigidbody;
            dir = (m_CPoints[i].point - (Vector2)transform.position).normalized;
            rig2D.AddForceAtPosition(m_ShockwareSize * dir, m_CPoints[i].point, ForceMode2D.Impulse);
        }
    }
   
}
