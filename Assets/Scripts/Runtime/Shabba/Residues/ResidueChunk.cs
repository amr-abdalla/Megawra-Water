using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResidueChunk : MonoBehaviour
{
	[SerializeField] private List<ResidueParticle> residueParticles = null;

	public IReadOnlyList<ResidueParticle> GetResidueParticles()
	{
		InitResidueParticles();
		return residueParticles;
	}

	private void Start()
	{
		InitResidueParticles();
	}

	private void InitResidueParticles()
	{
		//if (null != residueParticles) return;

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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent(out RigidbodyMovement playerMovement)) // figure condition out later
		{
			PushAllParticles(2f, playerMovement.GetComponent<Rigidbody2D>().velocity.normalized);		
		}
	}

	public void PushAllParticles(float value, Vector3 pushDirection)
	{
		foreach (var particle in residueParticles)
		{
			particle.GetComponent<ResidueParticleMovement>().GetPushed(value, pushDirection);
		}
	}
}
