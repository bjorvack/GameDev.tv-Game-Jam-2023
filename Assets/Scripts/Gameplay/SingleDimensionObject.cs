using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDimensionObject : MonoBehaviour
{
    private Collider2D collider;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Dimension dimension;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        PlayerController.dimensionChanged += OnDimensionChanged;

        DoDimensionHop(FindObjectOfType<PlayerController>().GetDimension());
    }

    private void OnDimensionChanged(Dimension dimension)
    {
        DoDimensionHop(dimension);
    }

    private void DoDimensionHop(Dimension dimension)
    {
        Debug.Log("Start dimension hop");
        if (dimension != this.dimension)
        {
            Debug.Log("Hide block");
            collider.isTrigger = true;
            spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                0.5f
            );
        } else
        {
            Debug.Log("show block");
            collider.isTrigger = false;
            spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                1f
            );
        }
    }
}
