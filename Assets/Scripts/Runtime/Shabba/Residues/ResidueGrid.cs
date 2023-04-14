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
	private const float _AdjacentPushValueMultiplier = 0.5f;
	private const float _AdjacentPushSpeedMultiplier = 0.5f;

	[SerializeField] private List<ResidueCell> residueCells;

	#region PUBLIC API
	public IReadOnlyList<ResidueCell> GetResidueCells() => residueCells;

	public IReadOnlyList<ResidueCell> GetAdjacentCells(ResidueCell residueCell)
	{
		return GetResidueCells().Where(cell => Vector2Int.Distance(cell.position, residueCell.position) == 1).ToList();
	}

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
			cell.residueChunk.OnGetPushed += PushAdjacentCells;
			cell.residueChunk.OnDestroy += RemoveCellWithChunk;
		}
	}

	private void RemoveCellWithChunk(ResidueChunk residueChunk)
	{
		var cellToRemove = residueCells.FirstOrDefault(cell => cell.residueChunk.Equals(residueChunk));
		residueCells.Remove(cellToRemove);
	}

	private void PushAdjacentCells(ResidueChunk residue, Vector3 direction, float pushValue, float pushSpeed)
	{
		var cellsToPush = GetAdjacentCells(residueCells.First(cell => cell.residueChunk == residue));

		foreach (ResidueCell cell in cellsToPush)
		{
			if (cell.residueChunk.GetResidueParticles()[0].particleMovement.currentPush is null)
			{
				cell.residueChunk.PushAllParticles(pushValue * _AdjacentPushValueMultiplier, direction, pushSpeed * _AdjacentPushSpeedMultiplier);
			}
		}
	}
	#endregion

}
