using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public delegate void StateAction();

public interface IState
{
    string Name { get; }
    void Enter();
    void Exit();
    void Update();
}

public class StateMachine
{
    private IState[] states;
    private IState currentState;

    public StateMachine(IState[] states)
    {
        this.states = states;
    }

    public void SetStateUnconditional(string name)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        foreach (IState state in states)
        {
            if (state.Name == name)
            {
                currentState = state;
                currentState.Enter();
                return;
            }
        }
        Debug.LogError("State not found: " + name);
    }

    public void SetStateConditional(string from, string to)
    {
        if (currentState == null)
            Debug.LogError("Current state is null");
        if (currentState.Name == from)
            SetStateUnconditional(to);
    }

    public void AddTransition(string from, string to, UnityEvent transitionEvent)
    {
        // check if from and to states exist
        if (!Array.Exists(states, state => state.Name == from))
        {
            Debug.LogError("State not found: " + from);
            return;
        }
        if (!Array.Exists(states, state => state.Name == to))
        {
            Debug.LogError("State not found: " + to);
            return;
        }

        // lambda function to transition to the next state
        UnityAction transition = () =>
        {
            SetStateConditional(from, to);
        };

        transitionEvent.AddListener(transition);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
