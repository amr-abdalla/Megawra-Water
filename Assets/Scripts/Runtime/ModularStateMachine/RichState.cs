public abstract class RichState<T> : State
    where T : AbstractStateMachineDataProvider
{
    protected T dataProvider;

    public void setDataProvider(ref T i_dataProvider)
    {
        dataProvider = i_dataProvider;
    }
}
