using UnityEngine;

public class PlatformIdleState : State
{
    protected override void onStateInit() { }

    protected override void onStateEnter()
    {
        Debug.Log("Platform Idle State");
        goToMove();
    }

    protected override void onStateExit() { }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate() { }

    public override void ResetState() { }

    private void goToMove()
    {
        setState<PlatformMoveState>();
    }
}
