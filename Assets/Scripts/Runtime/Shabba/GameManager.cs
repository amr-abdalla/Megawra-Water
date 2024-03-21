using UnityEngine;

public class GameManager: MonoBehaviour
{
	public float StartTime { get; private set; }
	public System.Action OnLoseAction;
	public System.Action OnWinAction;

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
		OnLoseAction?.Invoke();
	}

	public void OnWin()
	{
		Pause();
		OnWinAction?.Invoke();
	}
}
