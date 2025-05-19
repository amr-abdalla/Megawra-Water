using UnityEngine;
using System.Collections.Generic;

public class SplashFeature : StateFeatureAbstract
{
    [SerializeField]
    private GameObject splashPrefab = null;

    [SerializeField]
    private Vector3 splashOffset = Vector3.zero;

    [SerializeField]
    private Quaternion splashRotation = Quaternion.identity;

    [SerializeField]
    private GameObject puddlePrefab = null;

    [SerializeField]
    bool spawnPuddle = false;

    [SerializeField]
    private Vector3 puddleOffset = Vector3.zero;

    // Instance pool for splash objects
    private Queue<GameObject> splashPool = new Queue<GameObject>();
    // Reserve pool for refilling splashPool
    private Queue<GameObject> splashReservePool = new Queue<GameObject>();

    // Instance pool for puddle objects
    private Queue<GameObject> puddlePool = new Queue<GameObject>();
    private Queue<GameObject> puddleReservePool = new Queue<GameObject>();

    private Vector3 voidPosition = new Vector3(500, 500, 0);
    protected override void Awake()
    {
        base.Awake();
        if (splashPrefab != null)
        {
            for (int i = 0; i < 1; i++)
            {
                GameObject obj = Instantiate(splashPrefab);
                obj.transform.position = voidPosition;
                splashReservePool.Enqueue(obj);
            }
        }
        if (puddlePrefab != null)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(puddlePrefab);
                obj.transform.position = voidPosition;
                puddleReservePool.Enqueue(obj);
            }
        }
    }

    protected override void OnEnter()
    {
        if (splashPrefab == null) return;
        GameObject splashObject = null;
        if (splashPool.Count == 0 && splashReservePool.Count > 0)
        {
            // Refill splashPool from reserve
            while (splashReservePool.Count > 0)
            {
                splashPool.Enqueue(splashReservePool.Dequeue());
            }
        }
        if (splashPool.Count > 0)
        {
            splashObject = splashPool.Dequeue();
            splashObject.transform.position = transform.position + splashOffset;
            splashObject.transform.rotation = splashRotation;
        }
        else
        {
            splashObject = Instantiate(splashPrefab, transform.position + splashOffset, splashRotation);
        }

        SpriteFrameSwapper splashFrameSwapper = splashObject.GetComponent<SpriteFrameSwapper>();
        if (splashFrameSwapper == null) return;
        splashFrameSwapper.Stop();
        splashFrameSwapper.StopLoop();
        splashFrameSwapper.OnLastFrameReached += () =>
        {
            splashReservePool.Enqueue(splashObject);
        };
        splashFrameSwapper.Play();
        if (spawnPuddle)
        {
            GameObject puddleObject = null;
            if (puddlePool.Count == 0 && puddleReservePool.Count > 0)
            {
                while (puddleReservePool.Count > 0)
                {
                    puddlePool.Enqueue(puddleReservePool.Dequeue());
                }
            }
            if (puddlePool.Count > 0)
            {
                puddleObject = puddlePool.Dequeue();
                puddleObject.transform.position = transform.position + puddleOffset;
                puddleObject.transform.rotation = Quaternion.identity;
                puddleObject.transform.SetParent(null);
            }
            else
            {
                puddleObject = Instantiate(puddlePrefab, transform.position + puddleOffset, Quaternion.identity);
                puddleObject.transform.SetParent(null);
            }
            SpriteFrameSwapper puddleFrameSwapper = puddleObject.GetComponent<SpriteFrameSwapper>();
            if (puddleFrameSwapper == null) return;
            puddleFrameSwapper.Stop();
            puddleFrameSwapper.StopLoop();
            puddleFrameSwapper.OnLastFrameReached += () =>
            {
                puddleReservePool.Enqueue(puddleObject);
            };
            puddleFrameSwapper.Play();
        }
    }
}