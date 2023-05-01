using UnityEngine;

[CreateAssetMenu(fileName = "ResidueChunkPhysicsConfig", menuName = "ScriptableObjects/ResidueChunkPhysicsConfig", order = 1)]
public class ResidueChunkPhysicsConfig : ScriptableObject
{
	[SerializeField] public float pushValueMultiplier = 0.1f;
	[SerializeField] public float pushSpeedMultiplier = 0.1f;
	[SerializeField] public float randomPushRotationRange = 20f;
	[SerializeField] public float adjacentPushValueMultiplier = 0.5f;
	[SerializeField] public float adjacentPushSpeedMultiplier = 0.5f;
	[SerializeField] string chunkLayer = "Chunk";

	public float PushValueMultiplier => pushValueMultiplier;
	public float PushSpeedMultiplier => pushSpeedMultiplier;
	public float RandomPushRotationRange => randomPushRotationRange;
	public float AdjacentPushValueMultiplier => adjacentPushValueMultiplier;
	public float AdjacentPushSpeedMultiplier => adjacentPushSpeedMultiplier;
	public string ChunkLayer => chunkLayer;

	public LayerMask GetLayerMask()
    {
		return LayerMask.GetMask(chunkLayer);
	}

	public float GetRandomRotation()
    {
		return Random.Range(-randomPushRotationRange, randomPushRotationRange);

	}

}
