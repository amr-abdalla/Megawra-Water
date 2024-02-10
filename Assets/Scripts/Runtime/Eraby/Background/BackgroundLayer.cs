using System;
using System.Collections;
using System.Collections.Generic;
using log4net.Config;
using Newtonsoft.Json;
using UnityEngine;

public class BackgroundLayer : MonoBehaviour
{
    // Instantiates Background sprites as player moves through the level

    [Header("Sprites and Anchors")]
    [SerializeField]
    private Sprite[] backgroundSprites = null;

    [SerializeField]
    private Transform groundAnchor = null;

    [SerializeField]
    private Transform player = null;

    [Header("Configurations")]
    [SerializeField]
    private float distanceToPlayer = 0f;

    [SerializeField]
    private float spriteScale = 1f;

    [SerializeField]
    private int orderInLayer = 0;

    [SerializeField]
    private Material material = null;

    [SerializeField]
    [Range(0f, 1f)]
    private float saturation = 0.5f;

    [SerializeField]
    [Range(0f, 10f)]
    private float maxGap = 4f;
    private const int MAX_SPRITES = 20;

    private List<GameObject> spriteRenderers = null;

    private int cur_sprite = 0;

    private Bounds bounds;

    private void Start()
    {
        spriteRenderers = new List<GameObject>(MAX_SPRITES);
        for (int i = 0; i < MAX_SPRITES; i++)
        {
            GameObject newSprite = new("Background " + i);
            newSprite.transform.parent = groundAnchor;
            newSprite.transform.position = groundAnchor.position;
            newSprite.transform.localScale = new Vector3(spriteScale, spriteScale, 1f);
            SpriteRenderer renderer = newSprite.AddComponent<SpriteRenderer>();
            renderer.sortingLayerName = "Background_Fore";
            renderer.sortingOrder = orderInLayer;
            if (material != null)
                renderer.material = material;
            newSprite.SetActive(false);
            spriteRenderers.Add(newSprite);
        }

        bounds.min = groundAnchor.position.x;
        bounds.max = groundAnchor.position.x;
    }

    private void Update()
    {
        // check if we need to place a new sprite

        if (player.position.x - distanceToPlayer < bounds.min)
        {
            bounds.min -= PlaceNewSprite(bounds.min);
        }
    }

    private Sprite GetRandomSprite()
    {
        if (backgroundSprites.Length == 1)
        {
            return backgroundSprites[0];
        }

        // we move the selected sprite to the end of the array and exclude the last array element from the pool
        ArraySegment<Sprite> pool = new(backgroundSprites, 0, backgroundSprites.Length - 1);
        float rand = UnityEngine.Random.value;
        int i = Mathf.FloorToInt(rand * pool.Count);
        Sprite ret = pool.Array[i];
        backgroundSprites[i] = backgroundSprites[^1];
        backgroundSprites[^1] = ret;
        return ret;
    }

    // returns the distance to the next sprite

    private float PlaceNewSprite(float x_pos)
    {
        float skip_gap = Mathf.Clamp(0, 1, Mathf.PerlinNoise(x_pos, 0f));
        if (skip_gap < (1 - saturation))
        {
            return skip_gap * maxGap;
        }

        float distance = maxGap * skip_gap;
        Sprite sprite = GetRandomSprite();
        Vector3 position = groundAnchor.position;
        float half_sprite_width = sprite.bounds.size.x / 2f;
        position.x = x_pos - half_sprite_width;
        SpriteRenderer renderer = spriteRenderers[cur_sprite].GetComponent<SpriteRenderer>();
        spriteRenderers[cur_sprite].SetActive(true);
        spriteRenderers[cur_sprite].transform.position = position;
        cur_sprite = (cur_sprite + 1) % MAX_SPRITES;
        renderer.sprite = sprite;
        renderer.sortingLayerName = "Background_Fore";

        // add horizontal offset due to the sprite's width
        distance += half_sprite_width * 2f;
        return distance;
    }
}
