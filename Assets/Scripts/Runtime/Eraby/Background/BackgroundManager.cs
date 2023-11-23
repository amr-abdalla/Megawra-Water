using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpriteFloatConfig
{
    public float Buildings;
    public float Palms;
    public float Bushes;
}

enum SpriteCategory
{
    Buildings,
    Palms,
    Bushes
}

public class BackgroundManager : MonoBehaviour
{
    // Instantiates Background sprites as player moves through the level

    [Header("Sprites and Anchors")]
    [SerializeField]
    private BackgroundSprites backgroundSprites = null;

    [SerializeField]
    private Transform groundAnchor = null;

    [SerializeField]
    private Transform player = null;

    [Header("Configurations")]
    [SerializeField]
    private float distanceToPlayer = 0f;

    [Header("Weights")]
    [SerializeField]
    private SpriteFloatConfig groundWeights;

    [SerializeField]
    private SpriteFloatConfig spriteDistances;

    private float lastSpriteX = 0f;

    private void Start()
    {
        lastSpriteX = groundAnchor.position.x;
    }

    private void Update()
    {
        // Player's forward direction is -x. lastSpriteX must always be atleast distanceToPlayer in the forward direction ahead of player.

        while (player.position.x - lastSpriteX < distanceToPlayer)
        {
            lastSpriteX -= placeNewSprite(lastSpriteX);
        }
    }

    private SpriteCategory getRandomSpriteCategory()
    {
        float totalWeight = groundWeights.Buildings + groundWeights.Palms + groundWeights.Bushes;
        float random = Random.Range(0f, totalWeight);
        if (random < groundWeights.Buildings)
            return SpriteCategory.Buildings;
        else if (random < groundWeights.Buildings + groundWeights.Palms)
            return SpriteCategory.Palms;
        else
            return SpriteCategory.Bushes;
    }

    private Sprite getRandomSprite(SpriteCategory category)
    {
        switch (category)
        {
            case SpriteCategory.Buildings:
                return backgroundSprites.Buildings[
                    Random.Range(0, backgroundSprites.Buildings.Length)
                ];
            case SpriteCategory.Palms:
                return backgroundSprites.Palms[Random.Range(0, backgroundSprites.Palms.Length)];
            case SpriteCategory.Bushes:
                return backgroundSprites.Bushes[Random.Range(0, backgroundSprites.Bushes.Length)];
            default:
                return null;
        }
    }

    // returns the distance to the next sprite
    // TODO: pool this
    private float placeNewSprite(float x_pos)
    {
        SpriteCategory category = getRandomSpriteCategory();
        Sprite sprite = getRandomSprite(category);
        float distance = spriteDistances.Buildings;
        switch (category)
        {
            case SpriteCategory.Buildings:
                distance = spriteDistances.Buildings;
                break;
            case SpriteCategory.Palms:
                distance = spriteDistances.Palms;
                break;
            case SpriteCategory.Bushes:
                distance = spriteDistances.Bushes;
                break;
        }
        Vector3 position = groundAnchor.position;
        float half_sprite_width = sprite.bounds.size.x / 2f;
        position.x = x_pos - half_sprite_width;
        GameObject newSprite = new();
        newSprite.transform.parent = groundAnchor;
        newSprite.transform.position = position;
        SpriteRenderer renderer = newSprite.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingLayerName = "Background_Fore";

        // add horizontal offset due to the sprite's width
        distance += half_sprite_width * 2f;
        return distance;
    }
}
