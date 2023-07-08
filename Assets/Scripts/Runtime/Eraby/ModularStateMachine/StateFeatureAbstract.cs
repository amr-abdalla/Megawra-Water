using UnityEngine;

public abstract class StateFeatureAbstract : MonoBehaviourBase
{
    StateMachine stateMachine = null;

    #region Public

    public void InitFeature(StateMachine i_stateMachine)
    {
        stateMachine = i_stateMachine;
        onInit();
    }

    public void OnStateEnter()
    {
        onEnter();
    }

    public void OnStateUpdate()
    {
        onUpdate();
    }

    public void OnStateFixedUpdate()
    {
        onFixedUpdate();
    }

    public void OnStateExit()
    {
        onExit();
    }
    #endregion

    #region Protected

    protected void setState<TState>()
        where TState : State
    {
        stateMachine.SetState<TState>();
    }

    protected virtual void onInit() { }

    protected virtual void onEnter() { }

    protected virtual void onUpdate() { }

    protected virtual void onFixedUpdate() { }

    protected virtual void onExit() { }
    #endregion
}
