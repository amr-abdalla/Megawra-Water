using UnityEngine;

public class ErabyStateMachine : RichStateMachine<ErabyStateMachineDataProvider>
{
    public void Reset()
    {
        if (DataProvider != null)
        {
            DataProvider.Reset();
        }
        else
        {
            Debug.LogError("DataProvider is null. Cannot reset state machine.");
        }
    }
}
