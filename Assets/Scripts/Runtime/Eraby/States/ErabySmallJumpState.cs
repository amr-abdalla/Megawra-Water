using UnityEngine;

public class ErabySmallJumpState : ErabyJumpState
{
    [SerializeField]
    float jumpVelocityY = 0f;

    protected override void onStateEnter()
    {
        initialVelocityY = jumpVelocityY;
        onBaseJumpStateEnter();
        initialVelocityX = 0;
        Debug.Log("Enter small jump");
    }

    protected override void checkHeight()
    {
        if (!enabled || launchRoutine != null)
        {
            return;
        }

        if (body.VelocityY <= 0)
        {
            setState<ErabySmallFallState>();
            return;
        }
    }
}
