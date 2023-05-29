using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 5.0f;

    private void Start()
    {
        var rigidBody = GetComponent<Rigidbody2D>();

        // Add velocity to the traffic
        rigidBody.velocity = new Vector2(-1, 0) * m_Speed;
        rigidBody.angularVelocity = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Traffic");
            collision.gameObject.GetComponent<PlayerController>().Die();
        }
    }
}
