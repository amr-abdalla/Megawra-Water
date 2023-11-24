using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

struct Bounds
{
    public float min;
    public float max;
}

public class CameraBackground : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private Sprite backgroundSprite;

    [SerializeField]
    private Transform yAnchor;
    private SpriteRenderer[] backgroundRenderers;

    private int numRenderers = 3;

    private Bounds bounds;

    // Background sprites should not move with the camera. As the camera moves, the background sprites should be updated to reflect the new camera position.
    // We check the camera's viewport bounds to determine if we need to update the background sprites.

    private void Start()
    {
        // Create the background renderers
        backgroundRenderers = new SpriteRenderer[numRenderers];
        for (int i = 0; i < numRenderers; i++)
        {
            GameObject backgroundObject = new("Background " + i);
            backgroundRenderers[i] = backgroundObject.AddComponent<SpriteRenderer>();
            backgroundRenderers[i].sprite = backgroundSprite;
            backgroundRenderers[i].sortingLayerName = "Sky";
            backgroundRenderers[i].sortingOrder = -10;
        }

        InitBounds();
    }

    private void InitBounds()
    {
        // Get the camera bounds
        Vector2 cameraMin = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 cameraMax = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        // assume sprite anchor is bottom center
        float spriteWidth = backgroundSprite.bounds.size.x;
        float cameraCenter = (cameraMax.x + cameraMin.x) / 2;

        // calculate the bounds
        bounds.min = cameraCenter - (1.5f) * spriteWidth;
        bounds.max = cameraCenter + (1.5f) * spriteWidth;

        // set the initial positions
        for (int i = 0; i < numRenderers; i++)
        {
            backgroundRenderers[i].transform.position = new Vector3(
                bounds.min + i * spriteWidth,
                yAnchor.position.y,
                -5
            );
        }
    }

    private void Update()
    {
        Vector2 cameraMin = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 cameraMax = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        float threshold = cameraMax.x - cameraMin.x;
        if (cameraMin.x < bounds.min + threshold || cameraMax.x > bounds.max - threshold)
        {
            UpdateBounds();
        }
    }

    private void UpdateBounds()
    {
        // get the sprite which intersects the camera bounds
        int spriteIndex = 0;
        Vector2 cameraMin = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 cameraMax = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        for (int i = 0; i < numRenderers; i++)
        {
            if (
                backgroundRenderers[i].bounds.max.x > cameraMin.x
                && backgroundRenderers[i].bounds.min.x < cameraMax.x
            )
            {
                spriteIndex = i;
                break;
            }
        }

        // place the other sprites on either side of the sprite which intersects the camera bounds
        bounds.min = backgroundRenderers[spriteIndex].bounds.min.x - backgroundSprite.bounds.size.x;
        bounds.max = backgroundRenderers[spriteIndex].bounds.max.x + backgroundSprite.bounds.size.x;

        for (int i = 0; i < numRenderers; i++)
        {
            backgroundRenderers[i].transform.position = new Vector3(
                bounds.min
                    + backgroundSprite.bounds.size.x / 2
                    + i * backgroundSprite.bounds.size.x,
                yAnchor.position.y,
                -5
            );
        }
    }
}
