using UnityEngine;

[CreateAssetMenu(fileName = "Particle", menuName = "Residue")]
public class ResidueData : ScriptableObject
{
	[field: SerializeField] public float size { get; private set; }
	[field: SerializeField] public float momentum { get; private set; }
	[field: SerializeField] public float score { get; private set; }

	public void ApplyForce(Rigidbody2D rigidbody2D)
	{
		rigidbody2D.AddForce(rigidbody2D.velocity.normalized * momentum, ForceMode2D.Impulse); // or push
	}

	public bool IsCollectable()
	{
		return true;
	}
}
