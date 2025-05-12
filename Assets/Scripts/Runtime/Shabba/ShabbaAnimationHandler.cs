using UnityEngine;

public class ShabbaAnimationHandler : MonoBehaviour
{
	[SerializeField] private ShabbaAnimationController shabbaAnimationController;
	[SerializeField] private ShabbaBonesHandler bonesHandler;
	private const float _maxRotationDiff = 4.39f; 

	private void OnScoreChanged() => shabbaAnimationController.UpdateCurrentAnimation();

	public void PlayDashAnimation() => shabbaAnimationController.PlayDashAnimation();

	public void PlayIdleAnimation() => shabbaAnimationController.PlayIdleAnimation();

	public void ResetAnimation() => shabbaAnimationController.ResetIdleAnimation();

	public void OnRotate(float rotationDiff)
	{
		var t = Mathf.InverseLerp(0, _maxRotationDiff, Mathf.Abs(rotationDiff));

		if (rotationDiff > 0)
		{
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
		else if (rotationDiff < 0)
		{
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		}

		bonesHandler.SetBones(t);
	}

	void Start()
	{
		shabbaAnimationController.Init();
		ScoreTracker.OnScoreChanged += OnScoreChanged;
	}

	private void OnDestroy()
	{
		shabbaAnimationController.DeregisterEvents();
	}
}
