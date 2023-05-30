using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    [SerializeField]
    private Transform follow;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(
            follow.position.x,
            transform.position.y,
            transform.position.z
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.Die();
        }
    }
}
