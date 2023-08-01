using UnityEngine;
public abstract class ErabyState : RichState<ErabyStateMachineDataProvider> { 
    [SerializeField] protected ErabyControls controls = null;
}
