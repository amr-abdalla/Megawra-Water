using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ErabyControls : Controls
{
	[SerializeField] ErabyInputActions inputActions;
	public Action JumpPressed;
	public Action JumpReleased;
	public Action DiveStarted;
	public Action Dive;
	public Action DiveReleased;

	public Action BounceStarted;
	public Action BounceReleased;

	public Action<float> MoveStarted;
	public Action MoveReleased;

	private bool jumpActive = false;
	private bool diveActive = false;

	private float moveValue = 0f;

	#region PUBLIC API

	public override void EnableControls()
	{
		if (locked)
			return;

		inputActions.Player.Dive.Enable();
		inputActions.Player.Jump.Enable();
		inputActions.Player.Bounce.Enable();
		inputActions.Player.Move.Enable();
	}

	public override void DisableControls()
	{
		if (locked)
			return;

		inputActions.Player.Dive.Disable();
		inputActions.Player.Jump.Disable();
		inputActions.Player.Bounce.Disable();
		inputActions.Player.Move.Disable();
	}

	public override void RemoveControls()
	{
		JumpPressed = null;
		JumpReleased = null;

		DiveStarted = null;
		Dive = null;
		DiveReleased = null;

		BounceStarted = null;
		BounceReleased = null;

		MoveStarted = null;
		MoveReleased = null;
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

	public bool isJumping()
	{
		return jumpActive;
	}

	public bool isDiving()
	{
		return diveActive;
	}

	public bool isMoving()
	{
		return moveValue != 0f;
	}

	public float MoveDirection()
	{
		return moveValue;
	}

	#endregion

	#region Protected Overrides

	protected override void initInputs()
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

	protected override void unregisterInputs()
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

	#endregion

	#region PRIVATE

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
		moveValue = obj.ReadValue<float>();

		if (moveValue == 0f)
			onMoveCanceled(obj);
		else
			MoveStarted?.Invoke(obj.ReadValue<float>());
	}

	private void onMoveCanceled(InputAction.CallbackContext obj)
	{
		moveValue = 0f;
		MoveReleased?.Invoke();
	}

	#endregion
}
