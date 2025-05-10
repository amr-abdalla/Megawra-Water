using UnityEngine;

public class ErabyStateMachine : RichStateMachine<ErabyStateMachineDataProvider>
{
    public void Reset()
    {
        if (DataProvider != null)
        {
            _dataProvider = new ErabyStateMachineDataProvider();
        }
    }
}
