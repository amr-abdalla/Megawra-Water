using System.Collections;
using UnityEngine;

public class ErabyBumpState : ErabyState
{
    private Coroutine bumpRoutine = null;

    [SerializeField]
    private PhysicsBody2D body = null;

    [SerializeField]
    private Collider2D bumper = null;

    protected override void onStateEnter()
    {
        Debug.Log(
            "Enter bump. Direction: "
                + dataProvider.bumpDirection
                + " Magnitude: "
                + dataProvider.bumpMagnitude
                + " Duration: "
                + dataProvider.bumpDuration
        );

        float bumpMagnitude = dataProvider.bumpMagnitude;
        Vector2 bumpDirection = dataProvider.bumpDirection;
        // make sure the bump direction is normalized
        bumpDirection = Vector2.right * Mathf.Sign(bumpDirection.x);
        // rotate the bump direction by 45 degrees "up"
        bumpDirection =
            Quaternion.Euler(0, 0, 45 * bumpDirection.x) * bumpDirection * bumpMagnitude;

        body.SetVelocityX(bumpDirection.x);
        body.SetVelocityY(bumpDirection.y);
        dataProvider.initialVelocityY = bumpDirection.y;
    }

    protected override void onStateInit() { }

    protected override void onStateUpdate() { }

    protected override void onStateFixedUpdate()
    {
        if (body.VelocityY <= 0)
            setState<ErabySmallFallState>();
    }

    protected override void onStateExit()
    {
        controls.EnableControls();
        this.DisposeCoroutine(ref bumpRoutine);
    }

    public override void ResetState()
    {
        onStateExit();
    }
}
