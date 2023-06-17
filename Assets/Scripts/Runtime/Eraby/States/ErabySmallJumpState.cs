using UnityEngine;

public class ErabySmallJumpState : ErabyAbstractJumpState
{
    protected override void onStateEnter()
    {
        base.onStateEnter();

        Debug.Log("Enter small jump");
    }

    protected override void checkHeight()
    {
        if (!enabled)
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
