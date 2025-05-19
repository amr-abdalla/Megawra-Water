using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField]
    protected float distanceToPlayer = 0f;

    [SerializeField]
    protected Transform groundAnchor = null;

    [SerializeField]
    protected Transform player = null;

    public bool ShouldSpawn = true;

    protected Vector3 initialGroundAnchorPostion;
    protected struct SpawnerBounds
    {
        public float min;
        public float max;
    }

    protected SpawnerBounds bounds;

    protected virtual void Awake()
    {
        initialGroundAnchorPostion = groundAnchor.position;

        bounds = new SpawnerBounds { min = groundAnchor.position.x, max = groundAnchor.position.x };
    }

    protected virtual void Update()
    {
        // check if we need to place a new sprite

        if (ShouldSpawn && player.position.x - distanceToPlayer < bounds.min)
        {
            bounds.min -= Spawn(bounds.min);
        }
    }

    protected virtual float Spawn(float x)
    {
        return 0f;
    }
}
