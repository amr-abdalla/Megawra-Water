using System;
using UnityEngine;

public class GameTimer
{
	private float currentSessionStartTime;

	public GameTimer() => currentSessionStartTime = Time.time;
	public void Reset() => currentSessionStartTime = Time.time;

	public void Pause()
	{
		Time.timeScale = 0;
		OnPause?.Invoke();
	}

	public void Resume()
	{
		Time.timeScale = 1;
		OnResume?.Invoke();
	}

	public float GetTimeElapsedInCurrentSession() => Time.time - currentSessionStartTime;

	public Action OnPause;
	public Action OnResume;
}
