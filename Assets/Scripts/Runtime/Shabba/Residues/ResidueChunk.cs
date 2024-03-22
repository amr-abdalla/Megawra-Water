using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ResidueChunk : MonoBehaviour
{
	[SerializeField] float adjacentChunkDetectionRadius = 4f;
	[SerializeField] ResidueChunkPhysicsConfig physicsConfig = null;

	public System.Action<ResidueChunk, Vector3, float, float> OnGetPushed;
	public System.Action<ResidueChunk> OnDestroy;
	public System.Action<ResidueParticle> OnParticleRemoved;

	private List<ResidueParticle> residueParticles = null;

	bool pushedThisFrame = false;

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
			ScoreTracker.MaxScore += residueParticle.GetResidueScore();
		}

	}

	public Dictionary<ResidueData.ResidueType, int> GetResiduesCountByTypes()
	{
		Dictionary<ResidueData.ResidueType, int> residueNumbersByTypes = new Dictionary<ResidueData.ResidueType, int>() { { ResidueData.ResidueType.Small, 0 }, { ResidueData.ResidueType.Medium, 0 }, { ResidueData.ResidueType.Large, 0 } };

		foreach (ResidueParticle residueParticle in residueParticles)
		{
			residueNumbersByTypes[residueParticle.GetResidueType()]++;
		}

		return residueNumbersByTypes;

	}

	#endregion

	#region UNITY

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out StateMachine otherBody))
		{
			Vector2 bodyVelocity = otherBody.GetComponent<Rigidbody2D>().velocity;
			float bodySpeed = bodyVelocity.magnitude;
			PushAllParticles(bodySpeed * physicsConfig.PushValueMultiplier, otherBody.transform.position, bodySpeed * physicsConfig.PushSpeedMultiplier);
		}
	}

	#endregion

	#region PRIVATE

	private void RemoveResidueParticleFromCachedList(ResidueParticle residueParticle)
	{
		residueParticle.OnCollect -= RemoveResidueParticleFromCachedList;
		residueParticles.Remove(residueParticle);
		OnParticleRemoved?.Invoke(residueParticle);

		if (residueParticles.Count == 0)
		{
			OnDestroy?.Invoke(this);
			Destroy(gameObject);
		}
	}

	private void MarkPushedToFalse()
	{
		pushedThisFrame = false;
	}

	private void PushAllParticles(float value, Vector3 playerPosition, float speed)
	{
		pushedThisFrame = true;
		StartCoroutine(this.InvokeAtEndOfFrame(MarkPushedToFalse));

		foreach (ResidueParticle particle in residueParticles)
		{
			Vector3 pushDirection = (particle.transform.position - playerPosition).normalized;
			particle.GetPushed(value, Quaternion.Euler(0,0,physicsConfig.GetRandomRotation()) * pushDirection, speed);
		}

		PushAdjacentChunks(playerPosition * physicsConfig.AdjacentPushValueMultiplier, value, speed * physicsConfig.AdjacentPushSpeedMultiplier);
		OnGetPushed?.Invoke(this, playerPosition, value, speed);
	}

	private void PushAdjacentChunks(Vector3 direction, float pushValue, float pushSpeed)
	{
		var adjacentChunks = GetAdjacentChunks();

		foreach (ResidueChunk chunk in adjacentChunks)
		{
			if (!chunk.pushedThisFrame)
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

	private void OnDrawGizmos()
	{
#if UNITY_EDITOR
		Color col = UnityEditor.Handles.color;
		UnityEditor.Handles.color = Color.green;

		UnityEditor.Handles.DrawWireDisc(transform.position, MathConstants.VECTOR_3_BACK, adjacentChunkDetectionRadius);

		UnityEditor.Handles.color = col;
#endif
	}

}
