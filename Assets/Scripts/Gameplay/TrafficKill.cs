using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficKill : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Traffic")
        {
            Destroy(collision.gameObject);
        }
    }
}
