using System.Collections;
using UnityEngine;

public class ErabyWalkState : MoveHorizontalAbstractState
{
    [SerializeField]
    private ErabyCollisionEvents collisionEvents;

    [SerializeField]
    PersistentErabyData persistentData = null;

    protected override void onStateEnter()
    {
        Debug.Log("Enter walk");

        if (!controls.isMoving())
            goToIdle();

        base.onStateEnter();
        initialVelocityX = 0;
        updateMoveVelocity(controls.MoveDirection());
        controls.EnableControls();
        controls.JumpPressed += goToJump;
        controls.MoveReleased += goToIdle;
        collisionEvents.OnBump += onBump;
    }

    protected override void onStateExit()
    {
        controls.JumpPressed -= goToJump;
        collisionEvents.OnBump -= onBump;
        controls.MoveReleased -= goToIdle;
        base.onStateExit();
    }

    protected override void onStateInit() { }

    protected override void onStateUpdate() { }

    private void goToJump()
    {
        stateMachine.SetState<ErabySmallLaunchState>();
    }

    // move to a new bump state. Call bumpSequence onStateEnter. Go to idle state when done.
    private void onBump(float bumpMagnitude, float bumpDuration, Vector2 bumpDirection)
    {
        persistentData.bumpMagnitude = bumpMagnitude;
        persistentData.bumpDuration = bumpDuration;
        persistentData.bumpDirection = bumpDirection;
        setState<ErabyBumpState>();
    }

    private void goToIdle()
    {
        Debug.Log("Go to idle");
        stateMachine.SetState<ErabyIdleState>();
    }
}
