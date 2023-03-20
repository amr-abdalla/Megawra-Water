using UnityEngine;

[CreateAssetMenu(fileName = "Particle", menuName = "Residue")]
public class ResidueData : ScriptableObject
{
	[field: SerializeField] public float Size { get; private set; }
	[field: SerializeField] public float Momentum { get; private set; }
	[field: SerializeField] public float Score { get; private set; }

	public virtual void ApplyForce(Rigidbody2D rigidbody2D)
	{
		rigidbody2D.AddForce(rigidbody2D.velocity.normalized * Momentum, ForceMode2D.Impulse); // or push
	}

	public virtual bool IsCollectable()
	{
		return true;
	}
}
