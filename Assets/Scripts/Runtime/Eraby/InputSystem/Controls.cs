//This is the class for handling inputs so that states can recieve simple events for their logic

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviourBase
{
    [SerializeField]
    float diveDispatchInterval = 0.2f;

    ErabyInputActions inputActions;
    public Action JumpPressed;
    public Action JumpReleased;
    public Action DiveStarted;
    public Action Dive;
    public Action DiveReleased;

    public Action BounceStarted;
    public Action BounceReleased;

    bool locked = false;
    Coroutine diveRoutine = null;

    protected override void Awake()
    {
        base.Awake();
        initInputs();
    }

    private void OnEnable()
    {
        EnableControls();
    }

    private void OnDisable()
    {
        DisableControls();
    }

    private void OnDestroy()
    {
        DisableControls();

        JumpPressed = null;
        JumpReleased = null;

        DiveStarted = null;
        Dive = null;
        DiveReleased = null;

        unregisterInputs();
    }

    #region PUBLIC API

    public void EnableControls()
    {
        if (locked)
            return;
        //Debug.LogError("Enable Controls");
        inputActions.Player.Dive.Enable();
        inputActions.Player.Jump.Enable();
    }

    public void DisableControls()
    {
        if (locked)
            return;
        //Debug.LogError("Disable Controls");
        inputActions.Player.Dive.Disable();
        inputActions.Player.Jump.Disable();
        this.DisposeCoroutine(ref diveRoutine);
    }

    public void SetLock(bool i_lock)
    {
        locked = i_lock;
    }

    // public float DiveDirection()
    // {
    //     return (inputActions.Player.Dive.enabled)? inputActions.Player.Dive.ReadValue<Vector2>().x:0f;
    // }

    #endregion

    #region PRIVATE

    void initInputs()
    {
        if (null == inputActions)
            inputActions = new ErabyInputActions();
        inputActions.Player.Dive.performed += onDiveStarted;
        inputActions.Player.Dive.canceled += onDiveCanceled;

        inputActions.Player.Jump.started += onJumpStarted;
        inputActions.Player.Jump.canceled += onJumpCanceled;
    }

    void unregisterInputs()
    {
        if (null == inputActions)
            return;
        inputActions.Player.Dive.performed -= onDiveStarted;
        inputActions.Player.Dive.canceled -= onDiveCanceled;

        inputActions.Player.Jump.started -= onJumpStarted;
        inputActions.Player.Jump.canceled -= onJumpCanceled;
    }

    private void onDiveStarted(InputAction.CallbackContext obj)
    {
        if (null == diveRoutine)
            diveRoutine = StartCoroutine(dispatchDive());
    }

    private void onDiveCanceled(InputAction.CallbackContext obj)
    {
        this.DisposeCoroutine(ref diveRoutine);
        DiveReleased?.Invoke();
    }

    private void onJumpStarted(InputAction.CallbackContext obj)
    {
        JumpPressed?.Invoke();
    }

    private void onJumpCanceled(InputAction.CallbackContext obj)
    {
        JumpReleased?.Invoke();
    }

    private IEnumerator dispatchDive()
    {
        DiveStarted?.Invoke();

        while (true)
        {
            yield return diveDispatchInterval <= 0f ? null : this.Wait(diveDispatchInterval);
            Dive?.Invoke();
        }
    }

    #endregion
}
