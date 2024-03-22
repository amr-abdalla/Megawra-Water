using UnityEngine;
using UnityEngine.UI;

public class FinalScoreUI : MonoBehaviour
{
	[System.Serializable]
	private struct SpriteScore
	{
		[SerializeField] public Sprite sprite;
		[SerializeField] private Vector2 ScorePercentageBound;

		public bool IsScoreFulfilled(float score, float maxScore)
		{
			var scorePercentage = score / maxScore * 100;

			return scorePercentage > ScorePercentageBound.x && scorePercentage <= ScorePercentageBound.y;

		}
	}

	[SerializeField] private SpriteScore[] spriteScores;

	[SerializeField] private Image UIImage;

	public void Setup()
	{
		foreach(var spriteScore in spriteScores)
		{
			if(spriteScore.IsScoreFulfilled(ScoreTracker.CurrentScore, ScoreTracker.MaxScore))
			{
				UIImage.sprite = spriteScore.sprite;
				return;
			}
		}
	}

}
