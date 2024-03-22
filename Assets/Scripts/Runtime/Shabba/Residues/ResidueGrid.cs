using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public struct ResidueCell
{
	public Vector2 position;
	public ResidueChunk residueChunk;
}

public class ResidueGrid : MonoBehaviourBase
{
	private const float _XSpacing = 11f;
	private const float _YSpacing = 11f;

	private Dictionary<ResidueData.ResidueType, int> AllResidueCount;
	private Dictionary<ResidueData.ResidueType, int> CurrentResidueCount;
	[SerializeField] private List<ResidueCell> residueCells;
	[SerializeField] private ShabbaAudioManager ShabbaAudioManager;

	#region PUBLIC API
	public IReadOnlyList<ResidueCell> GetResidueCells() => residueCells;

	#endregion

	#region UNITY
	private void Start()
	{
		ScoreTracker.MaxScore = 0;
		InitChunks();
	}

	#endregion

	#region PRIVATE
	private void InitChunks()
	{
		AllResidueCount = new Dictionary<ResidueData.ResidueType, int>() { { ResidueData.ResidueType.Small, 0 }, { ResidueData.ResidueType.Medium, 0 }, { ResidueData.ResidueType.Large, 0 } };
		CurrentResidueCount = new(AllResidueCount);

		foreach (var cell in residueCells)
		{
			ResidueChunk residueChunk = Instantiate(cell.residueChunk);
			residueChunk.transform.position = new Vector3(_XSpacing * cell.position.x, _YSpacing * cell.position.y);
			residueChunk.Init();
			AddChunkToTotalCount(residueChunk);
			residueChunk.OnDestroy += RemoveCellWithChunk;
			residueChunk.OnParticleRemoved += OnParticleRemoved;
		}
	}

	private void OnParticleRemoved(ResidueParticle residueParticle)
	{
		CurrentResidueCount[residueParticle.GetResidueType()]--;

		if(residueParticle.GetResidueType() == ResidueData.ResidueType.Large)
		{
			ShabbaAudioManager.PlayCollectClip();
		}

		if(HasWon())
		{
			GlobalReferences.gameManager.OnWin();
		}
	}

	private bool HasWon() => CurrentResidueCount[ResidueData.ResidueType.Large] == 0;

	private Dictionary<ResidueData.ResidueType, int> CollectedResidues()
	{
		var Output = new Dictionary<ResidueData.ResidueType, int>() { { ResidueData.ResidueType.Small, 0 }, { ResidueData.ResidueType.Medium, 0 }, { ResidueData.ResidueType.Large, 0 } };

		foreach (var residue in AllResidueCount)
		{
			Output[residue.Key] = AllResidueCount[residue.Key] - CurrentResidueCount[residue.Key];
		}

		return Output;
	}

	private void AddChunkToTotalCount(ResidueChunk residueChunk)
	{
		Dictionary<ResidueData.ResidueType, int> residuesCountByTypes = residueChunk.GetResiduesCountByTypes();

		foreach(var residueCountByType in residuesCountByTypes)
		{
			AllResidueCount[residueCountByType.Key] += residueCountByType.Value;
			CurrentResidueCount[residueCountByType.Key] += residueCountByType.Value;
		}
	}

	private void RemoveCellWithChunk(ResidueChunk residueChunk)
	{
		var cellToRemove = residueCells.FirstOrDefault(cell => cell.residueChunk.Equals(residueChunk));
		residueCells.Remove(cellToRemove);
	}

	#endregion

}
