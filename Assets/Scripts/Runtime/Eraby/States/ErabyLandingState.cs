using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ErabyLandingState : ErabyAbstractLandingState
{
    private float maxLandingTime = 1f;

    Vector2 jumpUp = new Vector2(-1, 2).normalized;
    Vector2 jumpBack = new Vector2(-1, 2).normalized;
    Vector2 jumpForward = new Vector2(-1, 2).normalized;

    #region STATE API
    protected override void onStateEnter()
    {
        Debug.Log("Enter landing");

        // dataProvider.launchVelocityY =
        //     dataProvider.bounceVelocityMultiplier.y * dataProvider.launchVelocityY;

        Vector2 jumpDir =
            controls.MoveDirection() > 0.2f
                ? jumpBack
                : controls.MoveDirection() < -0.2f
                    ? jumpForward
                    : jumpUp;

        // Vector2 velocity =
        //     (
        //         new Vector2(dataProvider.landingVelocityX, dataProvider.launchVelocityY).magnitude
        //         * dataProvider.bounceVelocityMultiplier.magnitude
        //     ) * jumpDir;


        dataProvider.launchVelocityY = float.PositiveInfinity;
        Debug.Log("Landing velocity X: " + dataProvider.landingVelocityX);
        dataProvider.launchVelocityX = clampVelocityX(
            dataProvider.landingVelocityX * dataProvider.bounceVelocityMultiplier.x
        );
        Debug.Log("Bounce velocity: " + dataProvider.launchVelocityY);
        // landingTime = mapLaunchVelocityToLandingTime(dataProvider.launchVelocityY);
        base.onStateEnter();
    }

    #endregion

    #region PRIVATE


    override protected IEnumerator landingSequence()
    {
        yield return new WaitForSeconds(landingTime);
        // if (tapManager.isTapped())
        //     setState<ErabySuperLaunchState>();
        // else
        setState<ErabyLaunchState>();
    }

    protected override void applyTapMulipier()
    {
        // float newVelocityX = clampVelocityX(
        //     -Mathf.Abs(dataProvider.landingVelocityX) * tapMultiplier
        // );

        // dataProvider.launchVelocityX = newVelocityX;
        // dataProvider.launchVelocityY *= tapMultiplier;
    }

    private float mapLaunchVelocityToLandingTime(float yVel)
    {
        float maxYVelocity = accelerationData.MaxVelocityY;
        return yVel / maxYVelocity * maxLandingTime;
    }

    #endregion
}
