using UnityEngine;
using System.Collections;

class ErabyLaunchState : ErabyAbstractLaunchState
{
    protected override void goToJump()
    {
        setState<ErabyJumpState>();
    }
}
