using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResidueChunk : MonoBehaviour
{
	[SerializeField] private List<ResidueParticle> residueParticles = null;

	public System.Action<ResidueChunk, Vector3, float, float> OnGetPushed;
	public System.Action<ResidueChunk> OnDestroy;

	public const float _PushValueMultiplier = 0.1f;
	public const float _PushSpeedMultiplier = 0.05f;
	public const float _randomPushRotationRange = 150f;
	private const float _AdjacentPushValueMultiplier = 0.5f;
	private const float _AdjacentPushSpeedMultiplier = 0.5f;
	private const string _ChunkLayer = "Chunk";

	#region PUBLIC API
	public IReadOnlyList<ResidueParticle> GetResidueParticles()
	{
		InitResidueParticles();
		return residueParticles;
	}

	public void Init()
	{
		residueParticles.ForEach(particle => particle.particleMovement.Init());
	}
	#endregion

	#region UNITY
	private void Awake()
	{
		InitResidueParticles();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out RigidbodyMovement playerMovement)) // figure condition out later
		{
			Vector2 playerVelocity = playerMovement.GetComponent<Rigidbody2D>().velocity;
			PushAllParticles(playerVelocity.magnitude * _PushValueMultiplier, playerVelocity.normalized, playerVelocity.magnitude * _PushSpeedMultiplier);
		}
	}

	#endregion

	#region PRIVATE
	private void InitResidueParticles()
	{
		residueParticles = GetComponentsInChildren<ResidueParticle>().ToList();
		foreach (ResidueParticle residueParticle in residueParticles)
		{
			residueParticle.OnCollect += RemoveResidueParticleFromCachedList;
		}
	}

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

	private void PushAllParticles(float value, Vector3 pushDirection, float speed)
	{
		foreach (ResidueParticle particle in residueParticles)
		{
			particle.particleMovement.GetPushed(value, Quaternion.AngleAxis(Random.Range(-_randomPushRotationRange, _randomPushRotationRange), Vector2.right) * pushDirection, speed);
		}

		PushAdjacentChunks(pushDirection * _AdjacentPushValueMultiplier, value, speed * _AdjacentPushSpeedMultiplier);
		OnGetPushed?.Invoke(this, pushDirection, value, speed);
	}

	private void PushAdjacentChunks(Vector3 direction, float pushValue, float pushSpeed)
	{
		var adjacentChunks = GetAdjacentChunks();

		foreach (ResidueChunk chunk in adjacentChunks)
		{
			if (chunk.GetResidueParticles()[0].particleMovement.currentPush is null)
			{
				chunk.PushAllParticles(pushValue, direction, pushSpeed);
			}
		}
	}

	private IReadOnlyList<ResidueChunk> GetAdjacentChunks()
	{
		List<Collider2D> adjacentChunkColliders = Physics2D.OverlapCircleAll(transform.position, 8f, LayerMask.GetMask(_ChunkLayer)).ToList();
		adjacentChunkColliders.Remove(GetComponent<Collider2D>());

		return adjacentChunkColliders.Select(chunk => chunk.GetComponent<ResidueChunk>()).ToList();
	}

	#endregion
}
