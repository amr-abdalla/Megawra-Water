using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShabbaUIManager : MonoBehaviour
{
	[SerializeField] GameObject loseUI;
	[SerializeField] GameObject finalWinUI;
	[SerializeField] Button loseRestart;
	[SerializeField] Button loseMainMenu;
	[SerializeField] Button winNextLevel;
	[SerializeField] Button winMainMenu;
	[SerializeField] Button finalWinRestart;
	[SerializeField] Button finalWinMainMenu;

	[SerializeField] FinalScoreUI winUI;

	private void Start()
	{
		GameStateHandler.Instance.OnLoseAction += OnLose;
		GameStateHandler.Instance.OnWinAction += OnWin;
		loseRestart.onClick.AddListener(GameStateHandler.Instance.Restart);
		loseMainMenu.onClick.AddListener(GameStateHandler.Instance.GoToEntryScene);
		winMainMenu.onClick.AddListener(GameStateHandler.Instance.GoToEntryScene);
		winNextLevel.onClick.AddListener(GameStateHandler.Instance.GoToNextLevel);
		finalWinRestart.onClick.AddListener(GameStateHandler.Instance.Restart);
		finalWinMainMenu.onClick.AddListener(GameStateHandler.Instance.GoToEntryScene);
	}

	private void OnDestroy()
	{
		GameStateHandler.Instance.OnLoseAction -= OnLose;
		GameStateHandler.Instance.OnWinAction -= OnWin;
		loseRestart.onClick.RemoveListener(GameStateHandler.Instance.Restart);
		loseMainMenu.onClick.RemoveListener(GameStateHandler.Instance.GoToEntryScene);
		winMainMenu.onClick.RemoveListener(GameStateHandler.Instance.GoToEntryScene);
		winNextLevel.onClick.RemoveListener(GameStateHandler.Instance.GoToNextLevel);
		finalWinRestart.onClick.AddListener(GameStateHandler.Instance.Restart);
		finalWinMainMenu.onClick.AddListener(GameStateHandler.Instance.GoToEntryScene);
	}

	private void OnLose()
	{
		loseUI.SetActive(true);
		loseRestart.Select();
	}

	private void OnWin()
	{
		if (GameStateHandler.Instance.IsCurrentLevelTheFinalLevel)
		{
			finalWinUI.SetActive(true);
			finalWinMainMenu.Select();
		}
		else
		{
			winUI.gameObject.SetActive(true);
			winUI.Setup();
			winNextLevel.Select();
		}
	}
}
