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

    [SerializeField]
    private PhysicsBody2D erabyBody;

    [SerializeField]
    private float erabyInitialVelocityX;

    private void HandleLevelTransitionStart(int _i_level)
    {
        Debug.Log("Level Transition Start: " + _i_level);
        erabyTransform.position = erabyInitialPostion;
        erabyBody.SetVelocity(Vector2.zero);
        stateMachine.SetState<ErabyLevelTransitionIdleState>();
        stateMachine.Reset();
    }

    private void HandleLevelTransitionEnd(int _i_level)
    {
        Debug.Log("Level Transition End: " + _i_level);
        stateMachine.SetState<ErabyLevelStartFallState>();
    }

    private void Awake()
    {
        levelManager.OnNewLevelTransitionStart += HandleLevelTransitionStart;
        levelManager.OnNewLevelTransitionEnd += HandleLevelTransitionEnd;
    }
}
