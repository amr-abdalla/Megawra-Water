using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResidueChunk : MonoBehaviour
{
	[SerializeField] float adjacentChunkDetectionRadius = 8f;
	[SerializeField] ResidueChunkPhysicsConfig physicsConfig = null;

	public System.Action<ResidueChunk, Vector3, float, float> OnGetPushed;
	public System.Action<ResidueChunk> OnDestroy;

	private List<ResidueParticle> residueParticles = null;

	int pushedParticlesCount = 0;

	#region PUBLIC API
	public IReadOnlyList<ResidueParticle> GetResidueParticles()
	{
		return residueParticles;
	}

	public void Init()
	{
		residueParticles = GetComponentsInChildren<ResidueParticle>().ToList();
		foreach (ResidueParticle residueParticle in residueParticles)
		{
			residueParticle.Init();
			residueParticle.OnCollect += RemoveResidueParticleFromCachedList;
		}
	}

	#endregion

	#region UNITY

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out RigidbodyMovement otherBody))
		{
			Vector2 bodyVelocity = otherBody.CurrentVelocity;
			float bodySpeed = bodyVelocity.magnitude;
			PushAllParticles(bodySpeed * physicsConfig.PushValueMultiplier, bodyVelocity.normalized, bodySpeed * physicsConfig.PushSpeedMultiplier);
		}
	}

	#endregion

	#region PRIVATE

	private void RemoveResidueParticleFromCachedList(ResidueParticle residueParticle)
	{
		residueParticle.OnCollect -= RemoveResidueParticleFromCachedList;
		residueParticles.Remove(residueParticle);

		if (residueParticles.Count == 0)
		{
			OnDestroy?.Invoke(this);
			Destroy(gameObject);
		}
	}

	void onParticlePushEnded(ResidueParticle i_particle)
    {
		i_particle.OnPushEnded -= onParticlePushEnded;
		pushedParticlesCount++;

		if(pushedParticlesCount == residueParticles.Count)
        {
			pushedParticlesCount = 0;
			// all particle movement ended
		}
	}

	private void PushAllParticles(float value, Vector3 pushDirection, float speed)
	{
		pushedParticlesCount = 0;

		foreach (ResidueParticle particle in residueParticles)
		{
			particle.OnPushEnded += onParticlePushEnded;
			particle.GetPushed(value, Quaternion.Euler(0,0,physicsConfig.GetRandomRotation()) * pushDirection, speed);
		}

		PushAdjacentChunks(pushDirection * physicsConfig.AdjacentPushValueMultiplier, value, speed * physicsConfig.AdjacentPushSpeedMultiplier);
		OnGetPushed?.Invoke(this, pushDirection, value, speed);
	}

	private void PushAdjacentChunks(Vector3 direction, float pushValue, float pushSpeed)
	{
		var adjacentChunks = GetAdjacentChunks();

		foreach (ResidueChunk chunk in adjacentChunks)
		{
			if (chunk.pushedParticlesCount == 0)
			{
				chunk.PushAllParticles(pushValue, direction, pushSpeed);
			}
		}
	}

	private IReadOnlyList<ResidueChunk> GetAdjacentChunks()
	{
		List<Collider2D> adjacentChunkColliders = Physics2D.OverlapCircleAll(transform.position, adjacentChunkDetectionRadius, physicsConfig.GetLayerMask()).ToList();
		adjacentChunkColliders.Remove(GetComponent<Collider2D>());

		return adjacentChunkColliders.Select(chunk => chunk.GetComponent<ResidueChunk>()).ToList();
	}

	#endregion


	// Code review : draw a gizmo for the adjacentChunkDetectionRadius
}
