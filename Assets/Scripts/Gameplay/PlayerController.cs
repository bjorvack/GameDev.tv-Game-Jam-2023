using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Controls gameActions;

    private Rigidbody2D rb;

    private CapsuleCollider2D collider;

    private SpriteRenderer spriteRenderer;

    // Animation states
    private Animator animator;
    private string currentAnimationState;

    [SerializeField] 
    private Transform feetTransform;

    [SerializeField]
    private float jumpForce = 10f;

    [SerializeField]
    private float moveSpeed = 10f;

    private Vector2 movementDirection = Vector2.zero;

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
        collider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        DimensionHop();
    }

    private void Update()
    {
        bool isTouchingGround = IsTouchingSolidGround();
        animator.SetBool("onGround", isTouchingGround);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void DoDimensionHop(InputAction.CallbackContext obj)
    {
        // Check if currently inside a trigger for the oposite dimension
        // If true don't do a dimension hop
        if (collider.IsTouchingLayers(LayerMask.GetMask("Dimension - Blue")) && currentDimension == Dimension.Red)
        {
            return;
        }

        if (collider.IsTouchingLayers(LayerMask.GetMask("Dimension - Red")) && currentDimension == Dimension.Blue)
        {
            return;
        }

        DimensionHop();
    }

    private void DimensionHop()
    {
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
        movementDirection = obj.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.velocity = new Vector2(movementDirection.x * moveSpeed, rb.velocity.y);

        if (movementDirection.x != 0f)
        {
            spriteRenderer.transform.localScale = new Vector3(
                Mathf.Abs(spriteRenderer.transform.localScale.x) * (movementDirection.x < 0f ? -1f : 1f),
                spriteRenderer.transform.localScale.y,
                spriteRenderer.transform.localScale.z
            );

            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private bool IsTouchingSolidGround()
    {
        bool touchingGroundLayer = Physics2D.Raycast(feetTransform.position, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));
        bool touchingBlueDimension = Physics2D.Raycast(feetTransform.position, Vector2.down, 0.2f, LayerMask.GetMask("Dimension - Blue"));
        bool touchingRedDimension = Physics2D.Raycast(feetTransform.position, Vector2.down, 0.2f, LayerMask.GetMask("Dimension - Red"));

        Debug.Log("Touching Ground Layer: " + touchingGroundLayer);
        Debug.Log("Touching Blue Dimension: " + touchingBlueDimension);
        Debug.Log("Touching Red Dimension: " + touchingRedDimension);

        if (touchingGroundLayer ||
            (touchingRedDimension && currentDimension == Dimension.Red) ||
            (touchingBlueDimension && currentDimension == Dimension.Blue)
        )
        {
            return true;
        }

        return false;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        Debug.Log(IsTouchingSolidGround());
        // Only jump if the feet off the player are touching the ground Layer
        if (IsTouchingSolidGround()) {
            Debug.Log("Jump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("isJumping");
            animator.SetBool("OnGround", false);
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

    public bool IsTouchingInActiveDimension()
    {
        bool touchingBlueDimension = Physics2D.Raycast(feetTransform.position, Vector2.down, 0.2f, LayerMask.GetMask("Dimension - Blue"));
        bool touchingRedDimension = Physics2D.Raycast(feetTransform.position, Vector2.down, 0.2f, LayerMask.GetMask("Dimension - Red"));

        if (touchingBlueDimension && currentDimension == Dimension.Red) return true;
        if (touchingRedDimension && currentDimension == Dimension.Blue) return true;

        return false;
    }

    public void Die()
    {
        SceneManager.RestartScene();
    }
}
