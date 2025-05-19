using UnityEngine;

/// <summary>
/// Used to Deform the Shabba while rotating.
/// </summary>
public class ShabbaBonesHandler : MonoBehaviour
{
	[SerializeField] Transform bone2;
	[SerializeField] Transform bone3;
	[SerializeField] Transform bone4;

	[SerializeField] private AnimationCurve rotationCurve;

	// Max Rotation of each Bone Z coordinate. 
	private const float _bone2RotationMultiplier = -8.837f;
	private const float _bone3RotationMultiplier = -3.64f;
	private const float _bone4RotationMultiplier = -8.208f;

	public void SetBones(float t)
	{
		float bone2RotationZ = rotationCurve.Evaluate(t) * _bone2RotationMultiplier;
		float bone3RotationZ = rotationCurve.Evaluate(t) * _bone3RotationMultiplier;
		float bone4RotationZ = rotationCurve.Evaluate(t) * _bone4RotationMultiplier;

		bone2.localRotation = Quaternion.Euler(0, 0, bone2RotationZ);
		bone3.localRotation = Quaternion.Euler(0, 0, bone3RotationZ);
		bone4.localRotation = Quaternion.Euler(0, 0, bone4RotationZ);
	}
}
