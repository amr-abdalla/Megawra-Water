using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct ResidueCell
{
	public Vector2Int position;
	public ResidueChunk residueChunk;
}

public class ResidueGrid : MonoBehaviourBase
{
	private const float _XSpacing = 8f;
	private const float _YSpacing = 8f;

	[SerializeField] private List<ResidueCell> residueCells;

	#region PUBLIC API
	public IReadOnlyList<ResidueCell> GetResidueCells() => residueCells;

	#endregion

	#region UNITY
	private void Start()
	{
		InitChunks();
	}

	#endregion

	#region PRIVATE
	private void InitChunks()
	{
		foreach (var cell in residueCells)
		{
			cell.residueChunk.transform.position = new Vector3(_XSpacing * cell.position.x, _YSpacing * cell.position.y);
			cell.residueChunk.Init();
			cell.residueChunk.OnDestroy += RemoveCellWithChunk;
		}
	}

	private void RemoveCellWithChunk(ResidueChunk residueChunk)
	{
		var cellToRemove = residueCells.FirstOrDefault(cell => cell.residueChunk.Equals(residueChunk));
		residueCells.Remove(cellToRemove);
	}

	#endregion

}
