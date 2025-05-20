using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameID
{
	Shabba,
	Eraby,
	None
}

public class MainMenuManager : MonoBehaviour
{
	private GameID _currentlySelectedGame = GameID.None;

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
	[SerializeField] private InputActionReference submitInput;
	[SerializeField] private Animator animator;

	private void GoToEraby()
	{
		StartCoroutine(TransitionManager.instance.TransitionToScene("ErabyLoadingScene", "ErabyPlayground"));
	}

	private void GoToShabba()
	{
		StartCoroutine(TransitionManager.instance.TransitionToScene("ShabbaLoadingScene", "ShabbaLevel1"));
	}

	private void OnEnable()
	{
		selectionInput.action.Enable();
		submitInput.action.Enable();
		selectionInput.action.performed += OnSelectionInputRecieved;
		submitInput.action.performed += OnSubmitInputRecieved;
	}

	private void OnDisable()
	{
		selectionInput.action.performed -= OnSelectionInputRecieved;
		submitInput.action.performed -= OnSubmitInputRecieved;
		selectionInput.action.Disable();
		submitInput.action.Disable();
	}

	private void OnSubmitInputRecieved(InputAction.CallbackContext action)
	{
		if (TransitionManager.instance.IsTransitioning)
		{
			return;
		}

		switch (CurrentlySelectedGame)
		{
			case GameID.Eraby:
				GoToEraby();
				break;

			case GameID.Shabba:
				GoToShabba();
				break;
		}
	}

	private void OnSelectionInputRecieved(InputAction.CallbackContext action)
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
		if (currentlySelectedGame == GameID.Eraby)
		{
			animator.SetTrigger("Select Eraby");
		}
		else
		{
			animator.SetTrigger("Select Shabba");
		}
	}

}
