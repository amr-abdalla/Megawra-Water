using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShabbaControls : Controls
{
	[SerializeField] private InputActionReference moveInput;
	[SerializeField] private InputActionReference PushInput;

	private Action<InputAction.CallbackContext> currentMoveAction;
	private Action<InputAction.CallbackContext> currentPushAction;

	public override void DisableControls() => moveInput.action.Disable();

	public override void EnableControls() => moveInput.action.Enable();

	public override void RemoveControls() => unregisterInputs();

	public void RegisterMoveInput(Action<InputAction.CallbackContext> action)
	{
		moveInput.action.performed -= currentMoveAction;
		currentMoveAction = action;
		moveInput.action.performed += currentMoveAction;
	}

	public void RegisterPushInput(Action<InputAction.CallbackContext> action)
	{
		PushInput.action.performed -= currentPushAction;
		currentPushAction = action;
		PushInput.action.performed += currentPushAction;
	}

	protected override void initInputs()
	{
		moveInput.action.performed += currentMoveAction;
		PushInput.action.performed += currentPushAction;
	}

	protected override void unregisterInputs()
	{
		moveInput.action.performed -= currentMoveAction;
		PushInput.action.performed -= currentPushAction;
	}
}
