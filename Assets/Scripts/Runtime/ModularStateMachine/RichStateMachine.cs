using UnityEngine;

public abstract class RichStateMachine<T> : StateMachine
    where T : AbstractStateMachineDataProvider, new()
{
    [SerializeField]
    protected T _dataProvider = null;

    private RichState<T> _currentRichState = null;

    protected override void Awake()
    {
        base.Awake();
        if (null == _dataProvider)
            _dataProvider = new T();
    }

    public T DataProvider => _dataProvider;


    public override void SetState(State i_state)
    {
        if (i_state is RichState<T> richState)
        {
            richState.setDataProvider(ref _dataProvider);
            _currentRichState = richState;
        }
        else
        {
            _currentRichState = null;
        }

        base.SetState(i_state);
    }
}
