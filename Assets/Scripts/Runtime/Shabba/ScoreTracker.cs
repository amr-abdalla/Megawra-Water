public static class ScoreTracker
{
	public static float CurrentScore = 0;
	public static float TotalScore = 0;
	public static float MaxScore = 0;

	public static float GetCurrentScorePercentage() => CurrentScore / MaxScore * 100f;

	public static void Reset()
	{
		CurrentScore = 0;
		TotalScore = 0;
		MaxScore = 0;
	}
}
