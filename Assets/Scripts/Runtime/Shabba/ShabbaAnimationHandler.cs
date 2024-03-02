using UnityEngine;

public class ShabbaAnimationHandler : MonoBehaviour
{
	[SerializeField] private SpriteFrameSwapper dashAnimation;
	[SerializeField] private SpriteFrameSwapper idleAnimation;
	[SerializeField] private BonesHandler bonesHandler;

	public void InvokeDashAnimation()
	{
		idleAnimation.Stop();
		dashAnimation.ResetAnimation();
		dashAnimation.Play();
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
		dashAnimation.Stop();
		idleAnimation.ResetAnimation();
		idleAnimation.Play();
	}

	void Start() => dashAnimation.OnLastFrameReached += OnFinishDashAnimation;

}
