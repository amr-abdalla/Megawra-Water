

using UnityEngine;

public class StateFeatureExample : StateFeatureAbstract
{
    protected override void onEnter()
    {
        base.onEnter();

        Debug.Log("on enter state");
    }
}