using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayer : AbstractSpawner
{
    // Instantiates Background sprites as player moves through the level

    [Header("Sprites and Anchors")]
    [SerializeField]
    private Sprite[] backgroundSprites = null;

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

    [SerializeField]
    private float noSpawnDistanceFromGoal = 40f; // Distance from goal where no backgrounds spawn

    [SerializeField]
    private Transform goalTransform = null; // Reference to the goal's transform

    private const int MAX_SPRITES = 20;

    private List<GameObject> spriteRenderers = null;

    private int cur_sprite = 0;

    [SerializeField]
    private LevelManager levelManager = null;

    protected override void Awake()

    {
        base.Awake();
        if (null == levelManager)
        {
            return;
        }
        levelManager.OnNewLevelTransitionStart += HandleLevelStart;

    }

    private void HandleLevelStart(int _i_level)
    {
        Reset();
        Init();
    }

    private void Init()
    {
        cur_sprite = 0;
        groundAnchor.position = initialGroundAnchorPostion;
        // Debug.Log("Ground Anchor Postion" + groundAnchor.position);

        bounds = new SpawnerBounds { min = groundAnchor.position.x, max = groundAnchor.position.x };


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
    }

    private void Reset()
    {
        if (null == spriteRenderers) return;
        spriteRenderers.ForEach(s => Destroy(s));
        spriteRenderers.Clear();



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

    protected override float Spawn(float x_pos)
    {
        if (null == spriteRenderers) return 0;
        // Prevent spawning if within noSpawnDistanceFromGoal of the goal
        if (goalTransform != null && Mathf.Abs(goalTransform.position.x - x_pos) < noSpawnDistanceFromGoal)
        {
            return maxGap; // Skip this spawn, return max gap to move forward
        }
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
