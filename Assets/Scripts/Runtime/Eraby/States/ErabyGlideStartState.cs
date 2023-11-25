using System.Collections;
using UnityEngine;

public class ErabyGlideStartState : State
{  
    private Coroutine glideRoutine = null;

    [SerializeField]
    protected PhysicsBody2D body = null;

    [SerializeField]
    float glideDuration = 0f;

    protected override void onStateEnter()
    {

        Debug.Log("Enter glide start");
        // controls.DisableControls();
        body.SetVelocityX(0);
        body.SetVelocityY(0);

        glideRoutine = StartCoroutine(glideSequence(glideDuration));
    }

    protected override void onStateExit()
    {

        this.DisposeCoroutine(ref glideRoutine);
    }

    public override void ResetState()
    {
        onStateExit();
    }

    protected override void onStateInit() { }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate()
    {
        if (!isEnabled)
            return;
        body.SetVelocityY(0);
    }

    private void goToGlide()
    {
        stateMachine.SetState<ErabyGlideState>();
    }

    private IEnumerator glideSequence(float glideDuration)
    {
        yield return new WaitForSeconds(glideDuration);
        goToGlide();
        this.DisposeCoroutine(ref glideRoutine);
    }
}
