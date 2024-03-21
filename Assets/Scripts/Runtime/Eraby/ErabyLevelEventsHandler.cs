using UnityEngine;

public class ErabyLevelEventsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    LevelManager levelManager;

    [SerializeField]
    private Vector3 erabyInitialPostion;

    [SerializeField]
    private Transform erabyTransform;

    [SerializeField]
    private ErabyStateMachine stateMachine;
    private void HandleLevelStart(int _i_level)
    {
        stateMachine.SetState<ErabyIdleState>();
        erabyTransform.position = erabyInitialPostion;

    }

    private void Awake()
    {
        levelManager.RegisterToLevelStart(HandleLevelStart);
    }
}
