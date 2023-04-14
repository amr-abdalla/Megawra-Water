using System;
using UnityEngine;

public class ResidueParticle : MonoBehaviour
{
	[SerializeField] private ResidueData residueData;

	public ResidueParticleMovement particleMovement { get; private set; }
	public event Action<ResidueParticle> OnCollect;

	#region UNITY
	private void Awake()
	{
		particleMovement = GetComponent<ResidueParticleMovement>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out RigidbodyMovement playerMovement) && residueData.IsCollectable()) //will figure out the condition later
		{
			GetCollected();
			playerMovement.Push(residueData.Momentum);

			Destroy(gameObject);
		}
	}

	#endregion

	#region PRIVATE
	private void GetCollected()
	{
		OnCollect?.Invoke(this);
	}

	#endregion

}
