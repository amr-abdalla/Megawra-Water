using UnityEngine;

[CreateAssetMenu(fileName = "ResidueParticlePhysicsConfig", menuName = "ScriptableObjects/ResidueParticlePhysicsConfig", order = 2)]
public class ResidueParticlePhysicsConfig : ScriptableObject
{
	[SerializeField] float returnToStartPositionDelayInSeconds = 1f;
	[SerializeField] float returnToStartPositionSpeed = 0.2f;

	public float ReturnToStartPositionDelayInSeconds => returnToStartPositionDelayInSeconds;
	public float ReturnToStartPositionSpeed => returnToStartPositionSpeed;
}
