using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using static Eraby.ErabyConstants;

public class ErabyStateController : MonoBehaviour
{
    private StateMachine stateMachine;

    public UnityEvent OnJumpStart = new UnityEvent();
    public UnityEvent OnJumpEnd = new UnityEvent();
    public UnityEvent OnFall = new UnityEvent();
    public UnityEvent OnDive = new UnityEvent();
    public UnityEvent OnBounceStart = new UnityEvent();
    public UnityEvent OnBounceEnd = new UnityEvent();
    public UnityEvent OnCrash = new UnityEvent();
    public UnityEvent OnCrashRecovery = new UnityEvent();
    public UnityEvent OnLose = new UnityEvent();
    private List<(string, string, UnityEvent)> transitions =
        new List<(string, string, UnityEvent)>();

    private JumpingState jumpingState = new JumpingState();
    private FlyingState flyingState = new FlyingState();
    private FallingState fallingState = new FallingState();
    private DivingState divingState = new DivingState();
    private BouncingState bouncingState = new BouncingState();
    private CrashingState crashingState = new CrashingState();
    private LosingState losingState = new LosingState();

    private void Awake()
    {
        stateMachine = new StateMachine(
            new IState[]
            {
                jumpingState,
                flyingState,
                fallingState,
                divingState,
                bouncingState,
                crashingState,
                losingState
            }
        );

        stateMachine.SetStateUnconditional(RUNNING);
        transitions.Add((RUNNING, JUMPING, OnJumpStart));
        transitions.Add((JUMPING, FLYING, OnJumpEnd));
        transitions.Add((FLYING, FALLING, OnFall));
        transitions.Add((FLYING, DIVING, OnDive));
        transitions.Add((FALLING, DIVING, OnDive));
        transitions.Add((DIVING, BOUNCING, OnBounceStart));
        transitions.Add((BOUNCING, FLYING, OnBounceEnd));
        transitions.Add((DIVING, CRASHING, OnCrash));
        transitions.Add((FALLING, CRASHING, OnCrash));
        transitions.Add((CRASHING, FLYING, OnCrashRecovery));

        foreach (var transition in transitions)
        {
            stateMachine.AddTransition(transition.Item1, transition.Item2, transition.Item3);
        }

        OnLose.AddListener(() => stateMachine.SetStateUnconditional(LOSING));
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
