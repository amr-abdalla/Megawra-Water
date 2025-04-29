using UnityEngine;
using UnityEngine.InputSystem;

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
		shabbaInputHandler.EnableControls(GoToMainMenu);
		OnLoseAction?.Invoke();
		gameTimer.Pause();
	}

	public void OnWin()
	{
		shabbaInputHandler.EnableControls(GoToNextLevel);
		OnWinAction?.Invoke();
		gameTimer.Pause();
	}

	private void GoToNextLevel(InputAction.CallbackContext _)
	{
		ScoreTracker.CurrentScore = 0;
		levelManager.GoToNextLevel();
		shabbaInputHandler.DisableControls();
		gameTimer.Resume();
	}

	private void GoToMainMenu(InputAction.CallbackContext _)
	{
		ResetGame();
		levelManager.GoToMainMenu();
		shabbaInputHandler.DisableControls();
		gameTimer.Resume();
	}
}
