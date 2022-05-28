using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class L : MonoBehaviour
{
    public bool m = true;
    private Rigidbody2D m_Rig;


    private void Awake()
    {
        m_Rig = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Debug.Log(collision.relativeVelocity.magnitude);
        }
    }
}
