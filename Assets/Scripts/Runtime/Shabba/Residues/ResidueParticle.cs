using System;
using UnityEngine;

public class ResidueParticle : MonoBehaviour
{
	[SerializeField] private ResidueData residueData;

	ResidueParticleMovement particleMovement = null;
	public event Action<ResidueParticle> OnCollect;
	public event Action<ResidueParticle> OnPushEnded;

	#region PUBLIC API

	public void Init()
    {
		particleMovement = GetComponent<ResidueParticleMovement>();

		if(null != particleMovement)
			particleMovement.Init();
	}

	public void GetPushed(float pushValue, Vector2 pushDirection, float speed)
	{
		if (null == particleMovement) return;
		particleMovement.GetPushed(pushValue, pushDirection, speed);
	}

	#endregion

	#region UNITY



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
