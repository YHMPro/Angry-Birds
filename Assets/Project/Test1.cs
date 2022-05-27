using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Farme;
public class Test1 : MonoBase
{
    public float Force = 5;
    [SerializeField]
    private List<GameObject> m_DestroyGoLi = null;
    private List<GameObject> DestroyGoLi
    {
        get
        {
            if (m_DestroyGoLi == null)
            {
                m_DestroyGoLi = new List<GameObject>();
            }
            return m_DestroyGoLi;
        }
    }
    protected override void Awake()
    {
        base.Awake();
       
       

    }

    public void Update()
    {
       if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach(var go in DestroyGoLi)
            {
                Vector3 flyDir = (go.transform.position - transform.position).normalized;
                Vector3 appPoint = go.transform.position;
                go.GetComponent<Rigidbody2D>().AddForceAtPosition(Force * flyDir, appPoint, ForceMode2D.Impulse);
            }
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            if (!DestroyGoLi.Contains(collision.gameObject))
            {            
                DestroyGoLi.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            ContactPoint2D[] contactPoint2Ds = new ContactPoint2D[2];
            collision.GetContacts(contactPoint2Ds);
            Debug.Log(contactPoint2Ds[1].point);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9 || collision.gameObject.layer == 10)
        {
            if (DestroyGoLi.Contains(collision.gameObject))
            {
                DestroyGoLi.Remove(collision.gameObject);
            }
        }
    }
}
