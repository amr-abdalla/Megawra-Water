using UnityEngine;
public abstract class ErabyState : RichState<ErabyStateMachineDataProvider> { 
    [SerializeField] protected ErabyControls controls = null;


    [SerializeField]
    protected AccelerationConfig2D accelerationData = null;
}
