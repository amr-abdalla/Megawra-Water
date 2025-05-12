using System;

public static class ScoreTracker
{
	private static float _currentScore = 0;
	public static float CurrentScore
	{
		get
		{
			return _currentScore;
		}

		set
		{
			_currentScore = value;
			OnScoreChanged?.Invoke();
		}
	}

	public static float TotalScore = 0;
	public static float MaxScore = 0;

	public static Action OnScoreChanged;
	public static float GetCurrentToMaxScorePercentage() => CurrentScore / MaxScore * 100f;

	public static void Reset()
	{
		CurrentScore = 0;
		TotalScore = 0;
		MaxScore = 0;
	}
}
