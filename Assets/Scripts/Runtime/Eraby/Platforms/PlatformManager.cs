using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
struct PoolData
{
    public GameObject prefab;
    public int numInPool;
}

public class PlatformManager : AbstractSpawner
{
    [SerializeField]
    PoolData[] platformPoolData;

    List<GameObject> platformPool = null;

    List<GameObject> spawnedPlatforms = null;

    [SerializeField]
    public float despawnDistance;

    [Serializable]
    struct DistanceConfig
    {
        public float min;
        public float max;
    }

    [SerializeField]
    private DistanceConfig gaps;

    void Start()
    {
        int num_platforms = platformPoolData.Select(data => data.numInPool).Sum();

        platformPool = new List<GameObject>(num_platforms);
        spawnedPlatforms = new List<GameObject>(num_platforms);

        Debug.Log("PlatformManager Start");
        Debug.Log("num_platforms: " + num_platforms);

        foreach (PoolData data in platformPoolData)
        {
            for (int i = 0; i < data.numInPool; i++)
            {
                GameObject spawned = Instantiate(data.prefab);
                spawned.transform.parent = transform;
                spawned.SetActive(false);

                platformPool.Add(spawned);
            }
        }
    }

    bool ShouldBeCleaned(GameObject platform)
    {
        if (platform.activeSelf)
        {
            return player.position.x - platform.transform.position.x < -despawnDistance;
        }
        return false;
    }

    void CleanUpPlatforms()
    {
        var should_clean = spawnedPlatforms.Where(ShouldBeCleaned).ToList();
        should_clean.ForEach(o => Despawn(o));
        platformPool.AddRange(should_clean);
        spawnedPlatforms = spawnedPlatforms.Except(should_clean).ToList();
        // if platformPool is empty, we force move one platfrom from spawnedPlatforms to platformPool
        if (platformPool.Count == 0 && spawnedPlatforms.Count > 0)
        {
            platformPool.Add(spawnedPlatforms[0]);
            /* spawnedPlatforms[0].SetActive(false); */
            spawnedPlatforms.RemoveAt(0);
        }
    }

    void Despawn(GameObject obj)
    {
        Vector3 despawn_pos = new Vector3(999999, 999999, 99999);
        obj.transform.position = despawn_pos;
    }

    protected override void Update()
    {
        int max = 5;


        spawnedPlatforms.Sort((a, b) => b.transform.position.x.CompareTo(a.transform.position.x));
        bounds.min = spawnedPlatforms.LastOrDefault()?.transform.position.x ?? bounds.min;
        while (player.position.x - distanceToPlayer < (bounds.min) && max-- > 0)
        {
            Debug.LogError($"{player.position.x} - {distanceToPlayer} < {bounds.min})");
            bounds.min = Spawn(bounds.min);
        }
    }

    protected override float Spawn(float x)
    {
        return SpawnPlatform(x);
    }


    float SpawnPlatform(float x)
    {
        float currentRandom = UnityEngine.Random.Range(gaps.min, gaps.max);
        CleanUpPlatforms();
        ShuffleList(platformPool);
        GameObject platform = platformPool[0];
        platformPool.RemoveAt(0);
        // tinker with the state machine so enable/disable doesn't mess things up
        platform.SetActive(true);
        platform.transform.position = new Vector3(
            x - currentRandom,
            groundAnchor.position.y,
            groundAnchor.position.z
        );
        spawnedPlatforms.Add(platform);
        return platform.transform.position.x;
    }

    void ShuffleList<T>(List<T> l)
    {
        // Fisher-Yale Shuffle
        for (int i = l.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (l[j], l[i]) = (l[i], l[j]);
        }
    }
}
