
using UnityEngine;

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


    protected override void OnEnter()
    {
        if (splashPrefab == null) return;
        GameObject splashObject = Instantiate(splashPrefab, transform.position + splashOffset, splashRotation);

        SpriteFrameSwapper splashFrameSwapper = splashObject.GetComponent<SpriteFrameSwapper>();
        if (splashFrameSwapper == null) return;
        splashFrameSwapper.Stop();
        splashFrameSwapper.StopLoop();
        splashFrameSwapper.OnLastFrameReached += () =>
        {
            Destroy(splashObject);
        };
        splashFrameSwapper.Play();
        if (spawnPuddle)
        {
            GameObject puddleObject = Instantiate(puddlePrefab, transform.position, Quaternion.identity);
            puddleObject.transform.SetParent(null);
            SpriteFrameSwapper puddleFrameSwapper = puddleObject.GetComponent<SpriteFrameSwapper>();
            if (puddleFrameSwapper == null) return;
            puddleFrameSwapper.Stop();
            puddleFrameSwapper.StopLoop();
            puddleFrameSwapper.Play();

        }
    }



}