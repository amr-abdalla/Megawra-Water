using UnityEngine;

public class ErabySmallJumpState : ErabyAbstractJumpState
{
    protected override void onStateEnter()
    {
        base.onStateEnter();

        Debug.Log("Enter small jump");
    }

    protected override void onJumpEnded()
    {
        if (!isEnabled)
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
