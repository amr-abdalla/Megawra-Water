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

    public Action<float> MoveStarted;
    public Action MoveReleased;

    bool locked = false;
    Coroutine diveRoutine = null;

    private bool jumpActive = false;
    private bool diveActive = false;

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

        BounceStarted = null;
        BounceReleased = null;

        MoveStarted = null;
        MoveReleased = null;

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
        inputActions.Player.Bounce.Enable();
        inputActions.Player.Move.Enable();
    }

    public void DisableControls()
    {
        if (locked)
            return;
        //Debug.LogError("Disable Controls");
        inputActions.Player.Dive.Disable();
        inputActions.Player.Jump.Disable();
        inputActions.Player.Bounce.Disable();
        inputActions.Player.Move.Disable();
        this.DisposeCoroutine(ref diveRoutine);
    }

    public void DisableMove()
    {
        if (locked)
            return;
        inputActions.Player.Move.Disable();
    }

    public void EnableMove()
    {
        if (locked)
            return;
        inputActions.Player.Move.Enable();
    }

    public void SetLock(bool i_lock)
    {
        locked = i_lock;
    }

    public bool isJumping()
    {
        return jumpActive;
    }

    public bool isDiving()
    {
        return diveActive;
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
        inputActions.Player.Dive.started += onDiveStarted;
        inputActions.Player.Dive.canceled += onDiveCanceled;

        inputActions.Player.Jump.started += onJumpStarted;
        inputActions.Player.Jump.canceled += onJumpCanceled;

        inputActions.Player.Bounce.started += onBounceStarted;
        inputActions.Player.Bounce.canceled += onBounceCanceled;

        inputActions.Player.Move.started += onMoveStarted;
        inputActions.Player.Move.canceled += onMoveCanceled;
    }

    void unregisterInputs()
    {
        if (null == inputActions)
            return;
        inputActions.Player.Dive.started -= onDiveStarted;
        inputActions.Player.Dive.canceled -= onDiveCanceled;

        inputActions.Player.Jump.started -= onJumpStarted;
        inputActions.Player.Jump.canceled -= onJumpCanceled;

        inputActions.Player.Bounce.started -= onBounceStarted;
        inputActions.Player.Bounce.canceled -= onBounceCanceled;

        inputActions.Player.Move.started -= onMoveStarted;
        inputActions.Player.Move.canceled -= onMoveCanceled;
    }

    private void onDiveStarted(InputAction.CallbackContext obj)
    {
        diveActive = true;
        DiveStarted?.Invoke();
    }

    private void onDiveCanceled(InputAction.CallbackContext obj)
    {
        diveActive = false;
        DiveReleased?.Invoke();
    }

    private void onJumpStarted(InputAction.CallbackContext obj)
    {
        jumpActive = true;
        JumpPressed?.Invoke();
    }

    private void onJumpCanceled(InputAction.CallbackContext obj)
    {
        jumpActive = false;
        JumpReleased?.Invoke();
    }

    private void onBounceStarted(InputAction.CallbackContext obj)
    {
        BounceStarted?.Invoke();
    }

    private void onBounceCanceled(InputAction.CallbackContext obj)
    {
        BounceReleased?.Invoke();
    }

    private void onMoveStarted(InputAction.CallbackContext obj)
    {
        MoveStarted?.Invoke(obj.ReadValue<float>());
    }

    private void onMoveCanceled(InputAction.CallbackContext obj)
    {
        MoveReleased?.Invoke();
    }

    #endregion
}
