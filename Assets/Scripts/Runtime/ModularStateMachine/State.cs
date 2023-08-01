using UnityEngine;

public abstract class State : MonoBehaviourBase
{
    protected StateMachine stateMachine = null;
    protected bool isInit = false;
    private StateFeatureAbstract[] features = null;


    private bool _isEnabled = false;

    #region PUBLIC API
    public void Initialize(StateMachine i_stateMachine, Controls i_controls)
    {
        if (isInit)
            return;

        stateMachine = i_stateMachine;
        InitializeControls(i_controls);

        features = GetComponentsInChildren<StateFeatureAbstract>(true);
        foreach (StateFeatureAbstract feature in features)
            feature.InitFeature(stateMachine);

        onStateInit();

        isInit = true;
    }

    public virtual void InitializeControls(Controls i_controls) { }

    public void EnterState()
    {
        foreach (StateFeatureAbstract feature in features)
            feature.OnStateEnter();
        _isEnabled = true;
        onStateEnter();
    }

    public void UpdateState()
    {
        foreach (StateFeatureAbstract feature in features)
            feature.OnStateUpdate();
        onStateUpdate();
    }

    public void FixedUpdateState()
    {
        foreach (StateFeatureAbstract feature in features)
            feature.OnStateFixedUpdate();
        onStateFixedUpdate();
    }

    public void ExitState()
    {
        foreach (StateFeatureAbstract feature in features)
            feature.OnStateExit();
        onStateExit();
        _isEnabled = false;
    }

    public abstract void ResetState();

    #endregion

    #region PROTECTED API

    protected bool isEnabled => _isEnabled;

    protected void setState(string i_id)
    {
        stateMachine.SetGenericState(i_id);
    }

    protected void setState<TState>()
        where TState : State
    {
        stateMachine.SetState<TState>();
    }

    protected abstract void onStateInit();

    protected abstract void onStateEnter();

    protected abstract void onStateExit();

    protected abstract void onStateUpdate();

    protected virtual void onStateFixedUpdate() { }

    #endregion

    public virtual void DrawGizmos() { }
}
