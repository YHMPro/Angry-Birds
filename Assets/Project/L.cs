using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class L : MonoBehaviour
{
    public bool m = true;
    private Rigidbody2D m_Rig;

    public float ForceSize = 5;
    private void Awake()
    {
        m_Rig = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_Rig.AddForceAtPosition(Vector2.up * ForceSize, transform.position,ForceMode2D.Impulse);
            Debug.Log(m_Rig.velocity.magnitude);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


    }
}
