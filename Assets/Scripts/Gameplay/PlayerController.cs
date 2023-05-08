using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Controls gameActions;

    private Rigidbody2D rb;

    private void Start()
    {
        gameActions = InputManager.inputActions;

        Debug.Log(gameActions);

        gameActions.Game.Jump.started += DoJump;
        gameActions.Game.Enable();

        rb = GetComponent<Rigidbody2D>();
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
}
