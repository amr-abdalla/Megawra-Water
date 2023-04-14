using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResidueChunk : MonoBehaviour
{
	[SerializeField] private List<ResidueParticle> residueParticles = null;

	public Action<ResidueChunk, Vector3, float, float> OnGetPushed;
	public Action<ResidueChunk> OnDestroy;

	public const float _PushValueMultiplier = 0.1f;
	public const float _PushSpeedMultiplier = 0.05f;

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

	public void PushAllParticles(float value, Vector3 pushDirection, float speed)
	{
		foreach (ResidueParticle particle in residueParticles)
		{
			particle.particleMovement.GetPushed(value, pushDirection, speed);
		}

		OnGetPushed?.Invoke(this, pushDirection, value, speed);
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

		if(residueParticles.Count == 0)
		{
			OnDestroy?.Invoke(this);
			Destroy(gameObject);
		}
	}

	#endregion
}
