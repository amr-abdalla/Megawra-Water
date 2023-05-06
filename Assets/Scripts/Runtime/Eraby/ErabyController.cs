using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.Events;

public class ErabyController : MonoBehaviour
{
    const string RUNNING = "Running";
    const string JUMPING = "Jumping";
    const string FLYING = "Flying";
    const string FALLING = "Falling";
    const string DIVING = "Diving";
    const string BOUNCING = "Bouncing";
    const string CRASHING = "Crashing";
    const string LOSING = "Losing";
    private StateMachine stateMachine;
    private Dictionary<string, ErabyState> states = new Dictionary<string, ErabyState>()
    {
        { RUNNING, new ErabyState(RUNNING) },
        { JUMPING, new ErabyState(JUMPING) },
        { FLYING, new ErabyState(FLYING) },
        { FALLING, new ErabyState(FALLING) },
        { DIVING, new ErabyState(DIVING) },
        { BOUNCING, new ErabyState(BOUNCING) },
        { CRASHING, new ErabyState(CRASHING) },
        { LOSING, new ErabyState(LOSING) },
    };

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

    private void Awake()
    {
        foreach (var state in states.Values)
        {
            state.EnterActions += () => Debug.Log("Enter " + state.Name);
            state.ExitActions += () => Debug.Log("Exit " + state.Name);
        }

        stateMachine = new StateMachine(states.Values.ToArray());
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
        transitions.Add((CRASHING, LOSING, OnLose));

        foreach (var transition in transitions)
        {
            stateMachine.AddTransition(transition.Item1, transition.Item2, transition.Item3);
        }
    }

    private void Update()
    {
        stateMachine.Update();
    }
}
