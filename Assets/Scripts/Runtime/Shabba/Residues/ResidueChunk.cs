using System.Collections.Generic;
using UnityEngine;

public class ResidueChunk : MonoBehaviour
{
	private List<ResidueParticle> residueParticles = new();

	public List<ResidueParticle> GetResidueParticles()
	{
		if (residueParticles.Count == 0)
		{
			GenerateResidueParticles();
		}

		return residueParticles;
	}

	private void GenerateResidueParticles()
	{
		foreach (ResidueParticle residueParticle in GetComponentsInChildren<ResidueParticle>())
		{
			residueParticles.Add(residueParticle);
			residueParticle.OnDestroyEvent += () => RemoveResidueParticleFromCachedList(residueParticle);
		}
	}

	private void RemoveResidueParticleFromCachedList(ResidueParticle residueParticle)
	{
		residueParticles.Remove(residueParticle);
	}
}
