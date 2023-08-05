using UnityEngine;

public class StateFeatureExample : StateFeatureAbstract
{
    protected override void OnEnter()
    {
        base.OnEnter();

        Debug.Log("on enter state");
    }
}
