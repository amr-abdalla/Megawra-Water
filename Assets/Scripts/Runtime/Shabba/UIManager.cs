using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject winUI;
	[SerializeField] GameObject loseUI;
	[SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] TextMeshProUGUI timeText;

	private void Awake()
	{
		GlobalReferences.gameManager.OnLoseAction += OnLose;
		GlobalReferences.gameManager.OnWinAction += OnWin;
	}

	private void OnDestroy()
	{
		GlobalReferences.gameManager.OnLoseAction -= OnLose;
		GlobalReferences.gameManager.OnWinAction -= OnWin;
	}

	private void OnLose()
	{
		loseUI.SetActive(true);
	}

	private void OnWin()
	{
		winUI.SetActive(true);
		scoreText.text = "Score " + ScoreTracker.CurrentScore.ToString();
		timeText.text = "Time " + ((int)(Time.time - GlobalReferences.gameManager.StartTime)).ToString();
	}
}
