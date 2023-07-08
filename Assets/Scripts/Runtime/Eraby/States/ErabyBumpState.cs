using System.Collections;
using UnityEngine;

public class ErabyBumpState : ErabyAbstractFallState
{
    private Coroutine bumpRoutine = null;

    protected override void onStateEnter()
    {
        base.onStateEnter();
        Debug.Log("Enter bump");
        controls.DisableControls();
        float bumpMagnitude = persistentData.bumpMagnitude;
        Vector2 bumpDirection = persistentData.bumpDirection;
        // make sure the bump direction is normalized
        bumpDirection = Vector2.right * Mathf.Sign(bumpDirection.x);
        // rotate the bump direction by 45 degrees "up"
        bumpDirection =
            Quaternion.Euler(0, 0, 45 * bumpDirection.x) * bumpDirection * bumpMagnitude;

        body.SetVelocityX(bumpDirection.x);
        body.SetVelocityY(bumpDirection.y);
        persistentData.initialVelocityY = bumpDirection.y;
        groundCheckEnabled = false;
        bumpRoutine = StartCoroutine(bumpSequence(persistentData.bumpDuration));
    }

    protected override void onStateExit()
    {
        base.onStateExit();
        controls.EnableControls();
        this.DisposeCoroutine(ref bumpRoutine);
    }

    public override void ResetState()
    {
        base.ResetState();
        onStateExit();
    }

    private IEnumerator bumpSequence(float bumpDuration)
    {
        controls.DisableControls();
        yield return new WaitForSeconds(bumpDuration);
        groundCheckEnabled = true;
        controls.EnableControls();
        this.DisposeCoroutine(ref bumpRoutine);
    }

    protected override void onDidEnterGround()
    {
        goToLanding<ErabyIdleState>();
    }
}
