using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameStateHandler : MonoBehaviour
{
	public static GameStateHandler Instance { get; private set; }

	public System.Action OnLoseAction;
	public System.Action OnWinAction;

	[SerializeField] private ShabbaInputHandler shabbaInputHandler;
	private ShabbaLevelManager levelManager;
	private GameTimer gameTimer;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		levelManager = new ShabbaLevelManager();
		gameTimer = new GameTimer();
		DontDestroyOnLoad(gameObject);
	}

	public void ResetGame()
	{
		ScoreTracker.Reset();
		gameTimer.Reset();
	}

	public void OnLose()
	{
		shabbaInputHandler.EnableControls(ClickSelectedButton);
		OnLoseAction?.Invoke();
		gameTimer.Pause();
	}

	public void OnWin()
	{
		shabbaInputHandler.EnableControls(ClickSelectedButton);
		OnWinAction?.Invoke();
		gameTimer.Pause();
	}

	public void GoToNextLevel()
	{
		ScoreTracker.CurrentScore = 0;
		levelManager.GoToNextLevel();
		shabbaInputHandler.DisableControls();
		gameTimer.Resume();
	}

	private void ClickSelectedButton(InputAction.CallbackContext _)
	{
		var currentSelected = EventSystem.current.currentSelectedGameObject;
		currentSelected?.GetComponent<Button>().onClick?.Invoke();
	}

	public void GoToMainMenu()
	{
		ResetGame();
		levelManager.GoToMainMenu();
		shabbaInputHandler.DisableControls();
		gameTimer.Resume();
	}

	public void Restart()
	{
		ResetGame();
		shabbaInputHandler.DisableControls();
		gameTimer.Resume();
		levelManager.GoToFirstLevel();
	}
}
