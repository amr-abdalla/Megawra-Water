using System.Collections;
using UnityEngine;

public class DropShadowFeature : StateFeatureAbstract
{
    [SerializeField]
    private DropShadowData dropShadowData = null;

    [SerializeField]
    private Transform shadowTransform = null;

    [SerializeField]
    private Transform playerTransform = null;

    protected override void OnEnter()
    {
        shadowTransform.gameObject.SetActive(true);
    }

    protected override void OnExit()
    {
        shadowTransform.gameObject.SetActive(false);
    }

    protected override void OnUpdate()
    {
        shadowTransform.position = new Vector3(
            playerTransform.position.x,
            dropShadowData.ShadowHeight,
            playerTransform.position.z
        );

        float playerHeight = playerTransform.position.y;

        float shadowScale =
            dropShadowData.ShadowScaleCurve.Evaluate(
                (playerHeight - dropShadowData.MinPlayerHeight)
                    / (dropShadowData.MaxPlayerHeight - dropShadowData.MinPlayerHeight)
            ) * (dropShadowData.MaxShadowScale - dropShadowData.InitialShadowScale)
            + dropShadowData.InitialShadowScale;

        shadowTransform.localScale = new Vector3(
            shadowScale,
            shadowTransform.localScale.y,
            shadowTransform.localScale.z
        );
    }
}
