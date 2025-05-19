using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateControls : Controls
{
	[SerializeField] private InputActionReference PushInput;
	private Action<InputAction.CallbackContext> currentPushAction;

	public override void DisableControls() => PushInput.action.Disable();

	public override void EnableControls() => PushInput.action.Enable();

	public override void RemoveControls() => PushInput.action.performed -= currentPushAction;

	protected override void initInputs() { }

	protected override void unregisterInputs() => PushInput.action.performed -= currentPushAction;

	public void SetAction(Action<InputAction.CallbackContext> action)
	{
		PushInput.action.performed -= currentPushAction;
		currentPushAction = action;
		PushInput.action.performed += currentPushAction;
	}

}
