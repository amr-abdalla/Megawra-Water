using UnityEngine;

public abstract class StateFeatureAbstract : MonoBehaviourBase
{
    StateMachine stateMachine = null;

    private bool _isEnabled = false;

    public bool IsEnabled => _isEnabled;

    #region Public

    public void InitFeature(StateMachine i_stateMachine)
    {
        stateMachine = i_stateMachine;
        OnInit();
    }

    public void OnStateEnter()
    {
        _isEnabled = true;
        OnEnter();
    }

    public void OnStateUpdate()
    {
        OnUpdate();
    }

    public void OnStateFixedUpdate()
    {
        OnFixedUpdate();
    }

    public void OnStateExit()
    {
        _isEnabled = false;
        OnExit();
    }
    #endregion

    #region Protected

    protected void SetState<TState>()
        where TState : State
    {
        stateMachine.SetState<TState>();
    }

    protected virtual void OnInit() { }

    protected virtual void OnEnter() { }

    protected virtual void OnUpdate() { }

    protected virtual void OnFixedUpdate() { }

    protected virtual void OnExit() { }
    #endregion
}
