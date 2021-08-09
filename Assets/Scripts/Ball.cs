using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    public Vector3 m_velocity;
    private MainManager mainManager;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (mainManager.m_Started)
        {
            m_velocity = m_Rigidbody.velocity;

            //after a collision we accelerate a bit
            m_velocity += m_velocity.normalized * 0.01f;

            //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
            if (Vector3.Dot(m_velocity.normalized, Vector3.up) < 0.1f)
            {
                m_velocity += m_velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
            }

            //max velocity
            if (m_velocity.magnitude > 3.0f)
            {
                m_velocity = m_velocity.normalized * 3.0f;
            }

            m_Rigidbody.velocity = m_velocity;
        }
    }
}
