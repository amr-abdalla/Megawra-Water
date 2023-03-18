using System;
using UnityEngine;

public class ResidueParticle : MonoBehaviour
{
	[SerializeField] private ResidueData residueData;

	public event Action OnDestroyEvent;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Player") //will figure out the condition later
		{
			residueData.ApplyForce(other.GetComponent<Rigidbody2D>());
		}
	}

	private void OnDestroy()
	{
		OnDestroyEvent?.Invoke();
	}

}
