using System;
using UnityEngine;

public class ResidueParticle : MonoBehaviour
{
	[SerializeField] private ResidueData residueData;

	public event Action<ResidueParticle> OnCollect;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out RigidbodyMovement playerMovement) && residueData.IsCollectable()) //will figure out the condition later
		{
			GetCollected();
			playerMovement.PushShabba(residueData.Momentum);

			Destroy(gameObject);
		}
	}

	private void GetCollected()
	{
		OnCollect?.Invoke(this);
	}

}
