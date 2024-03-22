using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager: MonoBehaviour
{
	public float StartTime { get; private set; }
	public System.Action OnLoseAction;
	public System.Action OnWinAction;
	[SerializeField] private ShabbaControls shabbaControls;
	[SerializeField] private ShabbaLevelManager levelManager;

	private void Awake()
	{
		Init();
	}

	public void Init()
	{
		//condition for levels here
		Resume();
		StartTime = Time.time;
		ScoreTracker.CurrentScore = 0;
	}

	public void Pause() => Time.timeScale = 0;

	public void Resume() => Time.timeScale = 1;

	public void OnLose()
	{
		Pause();
		shabbaControls.RemoveControls();
		shabbaControls.RegisterPushInput(Restart);
		OnLoseAction?.Invoke();
	}

	public void OnWin()
	{
		Pause();
		shabbaControls.RemoveControls();
		shabbaControls.RegisterPushInput(GoToNextLevel);
		OnWinAction?.Invoke();
	}

	private void GoToNextLevel(InputAction.CallbackContext obj)
	{
		levelManager.GoToNextLevel();
		shabbaControls.RemoveControls();
	}

	private void Restart(InputAction.CallbackContext obj)
	{
		levelManager.GoToMainMenu();
		shabbaControls.RemoveControls();
	}
}
