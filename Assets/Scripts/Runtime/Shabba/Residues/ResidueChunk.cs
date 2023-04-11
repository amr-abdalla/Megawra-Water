using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResidueChunk : MonoBehaviour
{
	[SerializeField] private List<ResidueParticle> residueParticles = null;
	public Action<ResidueChunk, Vector3, float> OnGetPushed;
	public float lastPushTime = 0f;

	public IReadOnlyList<ResidueParticle> GetResidueParticles()
	{
		InitResidueParticles();
		return residueParticles;
	}

	private void Awake()
	{
		InitResidueParticles();
	}

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
	}

	public void Init()
	{
		residueParticles.ForEach(particle => particle.particleMovement.Init());
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent(out RigidbodyMovement playerMovement)) // figure condition out later
		{
			PushAllParticles(playerMovement.GetComponent<Rigidbody2D>().velocity.magnitude * 0.5f, playerMovement.GetComponent<Rigidbody2D>().velocity.normalized, 2f);		
		}
	}

	public void PushAllParticles(float value, Vector3 pushDirection, float speed)
	{
		foreach (ResidueParticle particle in residueParticles)
		{
			particle.particleMovement.GetPushed(value, pushDirection, speed);
		}

		lastPushTime = Time.time;
		OnGetPushed?.Invoke(this, pushDirection, value);
	}
}
