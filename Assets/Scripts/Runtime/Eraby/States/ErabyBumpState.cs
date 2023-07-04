using System.Collections;
using UnityEngine;

public class ErabyBumpState : State
{
    private Coroutine bumpRoutine = null;

    [SerializeField]
    protected PhysicsBody2D body = null;

    [SerializeField]
    private PersistentErabyData persistentData = null;

    protected override void onStateEnter()
    {
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

        bumpRoutine = StartCoroutine(bumpSequence(persistentData.bumpDuration));
    }

    protected override void onStateExit()
    {
        controls.EnableControls();
        this.DisposeCoroutine(ref bumpRoutine);
    }

    private void goToIdle()
    {
        stateMachine.SetState<ErabyIdleState>();
    }

    protected override void onStateFixedUpdate() { }

    public override void ResetState()
    {
        onStateExit();
    }

    protected override void onStateInit() { }

    protected override void onStateUpdate() { }

    private IEnumerator bumpSequence(float bumpDuration)
    {
        yield return new WaitForSeconds(bumpDuration);
        body.SetVelocityX(0);
        controls.EnableControls();
        goToIdle();
        this.DisposeCoroutine(ref bumpRoutine);
    }
}
