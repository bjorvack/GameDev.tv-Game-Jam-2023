using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Controls gameActions;

    private Rigidbody2D rb;

    [SerializeField] 
    private Transform feetTransform;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private Dimension currentDimension;

    public static event Action<Dimension> dimensionChanged;

    private void Start()
    {
        gameActions = InputManager.inputActions;

        gameActions.Game.Jump.started += DoJump;
        gameActions.Game.Move.performed += DoMove;
        gameActions.Game.Dimensionhop.started += DoDimensionHop;
        gameActions.Game.Enable();

        rb = GetComponent<Rigidbody2D>();
    }

    private void DoDimensionHop(InputAction.CallbackContext obj)
    {
        // Check if currently inside a trigger for the oposite dimension
        // If true don't do a dimension hop

        Debug.Log("Do dimension hop");
        switch (currentDimension)
        {
            case Dimension.Red:
                currentDimension = Dimension.Blue;
                break;
            case Dimension.Blue:
                currentDimension = Dimension.Red;
                break;
            default:
                currentDimension = Dimension.Red;
                break;
        }

        dimensionChanged?.Invoke(currentDimension);
    }

    private void DoMove(InputAction.CallbackContext obj)
    {
        Vector2 moveInput = obj.ReadValue<Vector2>();
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        // Only jump if the feet off the player are touching the ground Layer
        if (Physics2D.Raycast(feetTransform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground")) ||
            Physics2D.Raycast(feetTransform.position, Vector2.down, 0.1f, LayerMask.GetMask("Dimension - Red")) ||
            Physics2D.Raycast(feetTransform.position, Vector2.down, 0.1f, LayerMask.GetMask("Dimension - Blue"))
        ) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(feetTransform.position, feetTransform.position + Vector3.down * 0.1f);
    }

    public Dimension GetDimension()
    {
        return currentDimension;
    }
}
