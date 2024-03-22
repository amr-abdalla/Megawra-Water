using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;



[CreateAssetMenu(
    fileName = "PlatformManagerConfig",
    menuName = "Eraby/Platforms",
    order = 1
)]



public class PlatformManagerConfig : ScriptableObject
{
    // Start is called before the first frame update


    [Serializable]
    public struct PoolData
    {
        public GameObject prefab;
        public int numInPool;
    }

    [SerializeField]
    private List<PoolData> platforms;

    [SerializeField]
    private float despawnDistance;

    [Serializable]
    public struct DistanceConfig
    {
        public float min;
        public float max;
    }

    [SerializeField]
    private DistanceConfig gaps;


    public List<PoolData> Platforms => platforms;

    public DistanceConfig Gaps => gaps;

    public float DespawnDistance => despawnDistance;

}
