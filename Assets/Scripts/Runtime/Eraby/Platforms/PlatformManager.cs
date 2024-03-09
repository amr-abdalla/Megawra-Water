using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
struct PoolData{
    public GameObject prefab;
    public int numInPool;
}
public class PlatformManager : MonoBehaviour
{

    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    PoolData[] platformPoolData;

    [SerializeField]
    Transform groundTransform;

    [SerializeField]
    float distanceToPlayer;

    [SerializeField]
    float distanceBetweenSpawned;


    List<GameObject> platformPool = null;

    List<GameObject> spawnedPlatforms = null;

    [SerializeField]
    float spawnInterval = 1f;
    void Start()
    {
        int num_platforms = platformPoolData.Select(data => data.numInPool).Sum();

        platformPool = new List<GameObject>(num_platforms);
        spawnedPlatforms = new List<GameObject>(num_platforms);
        
        foreach (PoolData data in platformPoolData){
            for(int i = 0; i < data.numInPool; i++ ){
                GameObject spawned = Instantiate(data.prefab);
                spawned.SetActive(false);

                platformPool.Add(spawned);
            }
        }   
        
    }

    void Update()
    {
        
    }

    IEnumerator SpawnCoroutine(){
        while(true){
            SpawnPlatforms();
            yield return new WaitForSeconds(spawnInterval);
        }
    }



    bool ShouldBeCleaned(GameObject platform){
        // TODO
        return false;
    }
    void CleanUpPlatforms(){
        var should_clean = spawnedPlatforms.Where(ShouldBeCleaned).ToList();
        should_clean.ForEach(o => o.SetActive(false));
        platformPool.AddRange(should_clean);
        spawnedPlatforms = spawnedPlatforms.Except(should_clean).ToList();
    }


    void SpawnPlatforms(){
        ShuffleList(platformPool);

    }

    void ShuffleList<T>(List<T> l){
    // Fisher-Yale Shuffle
        for(int i = l.Count -1; i > 0; i--){
            int j = UnityEngine.Random.Range(0, i+1);
            (l[j], l[i]) = (l[i], l[j]);
        }
    }
}
