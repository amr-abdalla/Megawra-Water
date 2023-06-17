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
        float velocityX = Mathf.Cos(Mathf.Deg2Rad * 45) * bumpMagnitude;
        float velocityY = Mathf.Sin(Mathf.Deg2Rad * 45) * bumpMagnitude;
        body.SetVelocityX(velocityX);
        body.SetVelocityY(velocityY);
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
