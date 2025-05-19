using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ShabbaAnimationConfig
{
	public List<Frame<Sprite>> dashAnimation;
	public List<Frame<Sprite>> idleAnimation;
	public float minScorePercentage;
}

[System.Serializable]
public class ShabbaAnimationController
{
	[SerializeField] private ShabbaAnimationConfig[] shabbaAnimations;
	private float currentMinScorePercentage = 0;
	[SerializeField] private SpriteFrameSwapper dashAnimation;
	[SerializeField] private SpriteFrameSwapper idleAnimation;

	public void UpdateCurrentAnimation()
	{
		float currentScorePercentage = ScoreTracker.GetCurrentToMaxScorePercentage();

		foreach (ShabbaAnimationConfig shabbaAnimation in shabbaAnimations)
		{
			if (currentScorePercentage >= shabbaAnimation.minScorePercentage && currentMinScorePercentage < shabbaAnimation.minScorePercentage)
			{
				dashAnimation.ChangeFrames(shabbaAnimation.dashAnimation);
				idleAnimation.ChangeFrames(shabbaAnimation.idleAnimation);
				currentMinScorePercentage = shabbaAnimation.minScorePercentage;
			}
		}
	}

	public void PlayDashAnimation()
	{
		idleAnimation.Stop();
		dashAnimation.Play();
		dashAnimation.ResetAnimation();
	}

	public void PlayIdleAnimation()
	{
		dashAnimation.Stop();
		idleAnimation.Play();
		idleAnimation.ResetAnimation();
	}

	public void ResetIdleAnimation()
	{
		idleAnimation.Stop();
		Sprite firstSprite = idleAnimation.GetFirstFrame();
		idleAnimation.ResetAnimation(firstSprite);
	}

	public void Init()
	{
		UpdateCurrentAnimation();
		dashAnimation.OnLastFrameReached += PlayIdleAnimation;
	}

	public void DeregisterEvents()
	{
		dashAnimation.OnLastFrameReached -= PlayIdleAnimation;
	}
}