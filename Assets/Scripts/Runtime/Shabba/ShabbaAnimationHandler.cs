using UnityEngine;

public class ShabbaAnimationHandler : MonoBehaviour
{
	[SerializeField] private SpriteFrameSwapper[] dashAnimations;
	[SerializeField] private SpriteFrameSwapper[] idleAnimations;
	[SerializeField] private BonesHandler bonesHandler;


	private SpriteFrameSwapper GetCurrentDashAnimation()
	{
		float currentScorePercentage = ScoreTracker.GetCurrentScorePercentage();
		Debug.Log(currentScorePercentage);

		if (currentScorePercentage > 40f)
		{
			dashAnimations[0].Stop();
			dashAnimations[1].Stop();
			return dashAnimations[2];
		}

		if (currentScorePercentage > 20f)
		{
			dashAnimations[0].Stop();
			dashAnimations[2].Stop();
			return dashAnimations[1];
		}

		dashAnimations[1].Stop();
		dashAnimations[2].Stop();
		return dashAnimations[0];
	}

	private SpriteFrameSwapper GetCurrentIdleAnimation()
	{
		float currentScorePercentage = ScoreTracker.GetCurrentScorePercentage();

		if (currentScorePercentage > 40f)
		{
			idleAnimations[0].Stop();
			idleAnimations[1].Stop();
			return idleAnimations[2];
		}

		if (currentScorePercentage > 20f)
		{
			idleAnimations[0].Stop();
			idleAnimations[2].Stop();
			return idleAnimations[1];
		}

		idleAnimations[1].Stop();
		idleAnimations[2].Stop();
		return idleAnimations[0];
	}

	public void InvokeDashAnimation()
	{
		GetCurrentIdleAnimation().Stop();
		GetCurrentDashAnimation().ResetAnimation();
		GetCurrentDashAnimation().Play();
	}

	public void PlayIdleAnimation()
	{
		GetCurrentDashAnimation().Stop();
		GetCurrentIdleAnimation().ResetAnimation();
		GetCurrentIdleAnimation().Play();
	}

	public void ResetAnimation()
	{
		GetCurrentIdleAnimation().Stop();
		Sprite firstSprite = GetCurrentIdleAnimation().GetFirstFrame();
		GetCurrentIdleAnimation().ResetAnimation(firstSprite);
	}

	public void OnRotate(float rotationDiff)
	{
		var t = Mathf.InverseLerp(0, 4.39f, Mathf.Abs(rotationDiff));
		if (rotationDiff > 0) transform.localRotation = Quaternion.Euler(0, 0, 0);
		else if (rotationDiff < 0) transform.localRotation = Quaternion.Euler(0, 180, 0);
		bonesHandler.SetBones(t);
	}

	private void OnFinishDashAnimation()
	{
		GetCurrentDashAnimation().Stop();
		GetCurrentIdleAnimation().ResetAnimation();
		GetCurrentIdleAnimation().Play();
	}

	void Start()
	{
		foreach (var dashAnimation in dashAnimations)
		{
			dashAnimation.OnLastFrameReached += OnFinishDashAnimation;
		}
	}
}
