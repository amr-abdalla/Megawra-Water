using System.Collections;
using UnityEngine;

public class ErabyWalkState : MoveHorizontalAbstractState
{
    [SerializeField]
    private ErabyCollisionEvents collisionEvents;

    private Coroutine bumpRoutine = null;

    protected override void onStateEnter()
    {
        Debug.Log("Enter walk");
        base.onStateEnter();
        initialVelocityX = 0;
        body.SetVelocityX(0);
        controls.EnableControls();
        if (controls.isJumping())
            goToJump();
        controls.JumpPressed += goToJump;
        collisionEvents.OnBump += onBump;
        // collisionEvents.OnTrample += onBump;
    }

    protected override void onStateExit()
    {
        controls.JumpPressed -= goToJump;
        base.onStateExit();
    }

    protected override void onStateInit() { }

    protected override void onStateUpdate() { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            setState<ErabySmallJumpState>();
        }
    }

    private void goToJump()
    {
        stateMachine.SetState<ErabySmallJumpState>();
    }

    private void onBump(float bumpMagnitude, float bumpDuration)
    {
        if (bumpRoutine == null)
            bumpRoutine = StartCoroutine(bumpSequence(bumpMagnitude, bumpDuration));
    }

    private IEnumerator bumpSequence(float bumpMagnitude, float bumpDuration)
    {
        controls.DisableControls();
        float velocityX = Mathf.Cos(Mathf.Deg2Rad * 45) * bumpMagnitude;
        float velocityY = Mathf.Sin(Mathf.Deg2Rad * 45) * bumpMagnitude;
        body.SetVelocityX(velocityX);
        body.SetVelocityY(velocityY);
        yield return new WaitForSeconds(bumpDuration);
        body.SetVelocityX(0);
        controls.EnableControls();
        this.DisposeCoroutine(ref bumpRoutine);
    }
}
