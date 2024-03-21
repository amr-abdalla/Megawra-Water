using UnityEngine;

public static class GameManager
{

	public static float StartTime { get; private set; }
	public static System.Action OnLoseAction;
	public static System.Action OnWinAction;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void Init()
	{
		Resume();
		StartTime = Time.time;
		ScoreTracker.CurrentScore = 0;
	}

	public static void Pause() => Time.timeScale = 0;

	public static void Resume() => Time.timeScale = 1;

	public static void OnLose()
	{
		Pause();
		OnLoseAction?.Invoke();
	}

	public static void OnWin()
	{
		Pause();
		OnWinAction?.Invoke();
	}
}
