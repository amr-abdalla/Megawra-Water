using UnityEngine;

[CreateAssetMenu(fileName = "Particle", menuName = "Residue")]
public class ResidueData : ScriptableObject
{
	public enum ResidueType { Small, Medium, Large};

	[field: SerializeField] public float Size { get; private set; }
	[field: SerializeField] public float Momentum { get; private set; }
	[field: SerializeField] public float Score { get; private set; }
	[field: SerializeField] public ResidueType Type { get; private set; }


	public virtual bool IsCollectable()
	{
		return true;
	}
}
