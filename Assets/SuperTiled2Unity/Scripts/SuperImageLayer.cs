using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperTiled2Unity
{
    public class SuperImageLayer : SuperLayer
    {
        [ReadOnly]
        public string m_ImageFilename;

        private void Start()
        {
            Debug.Log("Repeating pattern");

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.drawMode = SpriteDrawMode.Tiled;
            spriteRenderer.size = new Vector2(
                spriteRenderer.size.x * 1000f,
                spriteRenderer.size.y
            );

            Debug.Log("Moving layer to back");

            transform.position = new Vector3(
              - (spriteRenderer.size.x / 2),
              transform.position.y,
              m_ParallaxX
            );
        }
    }
}
