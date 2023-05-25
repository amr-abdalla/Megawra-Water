public class ErabyDiveState : ErabyFallState
{
    protected override void onStateEnter()
    {
        body.SetVelocityY(0);
        controls.EnableControls();
    }

    protected override void onStateExit()
    {
        // controls.EnableControls();
        this.DisposeCoroutine(ref landingRoutine);
    }
}
