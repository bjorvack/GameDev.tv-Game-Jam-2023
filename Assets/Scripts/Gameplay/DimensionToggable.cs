using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RedDimension : MonoBehaviour
{
    [SerializeField]
    private Dimension dimension;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.dimensionChanged += OnDimensionChanged;

        DoDimensionHop(FindObjectOfType<PlayerController>().GetDimension());
    }

    private void OnDimensionChanged(Dimension dimension)
    {
        DoDimensionHop(dimension);
    }

    private void DoDimensionHop(Dimension dimension)
    {
        var tilemap = GetComponent<Tilemap>();
        var colliders = GetComponentsInChildren<PolygonCollider2D>();
        Debug.Log("Start dimension hop");
        if (dimension != this.dimension)
        {
            Debug.Log("Hide block");
            foreach (PolygonCollider2D collider in colliders)
            {
                collider.isTrigger = true;
            }

            tilemap.color = new Color(
                tilemap.color.r,
                tilemap.color.g,
                tilemap.color.b,
                0.5f
            );
        }
        else
        {
            Debug.Log("show block");
            foreach (PolygonCollider2D collider in colliders)
            {
                collider.isTrigger = false;
            }

            tilemap.color = new Color(
                tilemap.color.r,
                tilemap.color.g,
                tilemap.color.b,
                1f
            );
        }
    }
}
