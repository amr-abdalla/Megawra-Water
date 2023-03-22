using System;
using UnityEngine;

public class ResidueParticle : MonoBehaviour
{
	[SerializeField] private ResidueData residueData;

	public event Action OnDestroyEvent;
	public event Action OnCollect;


	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out RigidbodyMovement playerMovement) && residueData.IsCollectable()) //will figure out the condition later
		{
			GetCollected(playerMovement);
		}
	}

	private void GetCollected(RigidbodyMovement playerMovement)
	{
		Debug.Log("yoo");
		OnCollect?.Invoke();
		playerMovement.PushShabba(residueData.Momentum);
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		OnDestroyEvent?.Invoke();
	}

}
