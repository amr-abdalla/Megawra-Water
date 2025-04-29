using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShabbaInputHandler : MonoBehaviour
{
	[SerializeField] private GameStateControls gameStateControls;

	public void EnableControls(Action<InputAction.CallbackContext> action)
	{
		gameStateControls.EnableControls();
		gameStateControls.SetAction(action);
	}

	public void DisableControls() => gameStateControls.DisableControls();
}
