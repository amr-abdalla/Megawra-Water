using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShabbaUIManager : MonoBehaviour
{
	[SerializeField] GameObject loseUI;
	[SerializeField] Button loseRestartButton;
	[SerializeField] Button loseMainMenuButton;
	[SerializeField] Button winNextLevelButton;
	[SerializeField] Button winMainMenuButton;

	[SerializeField] FinalScoreUI winUI;

	private void Start()
	{
		GameStateHandler.Instance.OnLoseAction += OnLose;
		GameStateHandler.Instance.OnWinAction += OnWin;
		loseRestartButton.onClick.AddListener(GameStateHandler.Instance.Restart);
		loseMainMenuButton.onClick.AddListener(GameStateHandler.Instance.GoToMainMenu);
		winMainMenuButton.onClick.AddListener(GameStateHandler.Instance.GoToMainMenu);
		winNextLevelButton.onClick.AddListener(GameStateHandler.Instance.GoToNextLevel);
	}

	private void OnDestroy()
	{
		GameStateHandler.Instance.OnLoseAction -= OnLose;
		GameStateHandler.Instance.OnWinAction -= OnWin;
		loseRestartButton.onClick.RemoveListener(GameStateHandler.Instance.Restart);
		loseMainMenuButton.onClick.RemoveListener(GameStateHandler.Instance.GoToMainMenu);
		winMainMenuButton.onClick.RemoveListener(GameStateHandler.Instance.GoToMainMenu);
		winNextLevelButton.onClick.RemoveListener(GameStateHandler.Instance.GoToNextLevel);
	}

	private void OnLose()
	{
		loseUI.SetActive(true);
		loseRestartButton.Select();
	}

	private void OnWin()
	{
		winUI.gameObject.SetActive(true);
		winUI.Setup();
		winNextLevelButton.Select();
	}
}
