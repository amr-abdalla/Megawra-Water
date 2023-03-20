using System;
using UnityEngine;

public class ResidueParticle : MonoBehaviour
{
	[SerializeField] private ResidueData residueData;

	public event Action OnDestroyEvent;
	public event Action OnCollect;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player" && residueData.IsCollectable()) //will figure out the condition later
		{
			GetCollected(other);
		}
	}

	private void GetCollected(Collider collector)
	{
		OnCollect?.Invoke();
		residueData.ApplyForce(collector.GetComponent<Rigidbody2D>());
	}

	private void OnDestroy()
	{
		OnDestroyEvent?.Invoke();
	}

}
