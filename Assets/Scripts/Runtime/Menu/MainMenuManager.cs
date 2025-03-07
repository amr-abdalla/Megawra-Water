using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameID
{
    Shabba,
    Eraby
}

public class MainMenuManager : MonoBehaviour
{
    private GameID _currentlySelectedGame = GameID.Shabba;

	private GameID CurrentlySelectedGame
	{
		get
		{
			return _currentlySelectedGame;
		}

		set
		{
			if (_currentlySelectedGame == value)
			{
				return;
			}

			_currentlySelectedGame = value;
			OnSelectionChanged(_currentlySelectedGame);
		}
	}

	[SerializeField] private InputActionReference selectionInput;

	private void Awake()
	{
		selectionInput.action.Enable();
	}

	private void OnEnable()
	{
		selectionInput.action.performed += Action_performed;
	}

	private void OnDisable()
	{
		selectionInput.action.performed -= Action_performed;
	}

	private void Action_performed(InputAction.CallbackContext action)
	{
		float inputValue = action.ReadValue<float>();

		if (inputValue > 0)
		{
			SelectRight();
		}
		else
		{
			SelectLeft();
		}
	}

	private void SelectLeft()
	{
		if (CurrentlySelectedGame == GameID.Shabba)
		{
			return;
		}

		CurrentlySelectedGame = GameID.Shabba;
	}

	private void SelectRight()
	{
		if (CurrentlySelectedGame == GameID.Eraby)
		{
			return;
		}

		CurrentlySelectedGame = GameID.Eraby;
	}

	private void OnSelectionChanged(GameID currentlySelectedGame)
	{
		Debug.Log(currentlySelectedGame);
	}

}
