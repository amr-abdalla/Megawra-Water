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
		GameManager.OnLoseAction += OnLose;
		GameManager.OnWinAction += OnWin;
	}

	private void OnDestroy()
	{
		GameManager.OnLoseAction -= OnLose;
		GameManager.OnWinAction -= OnWin;
	}

	private void OnLose()
	{
		loseUI.SetActive(true);
	}

	private void OnWin()
	{
		winUI.SetActive(true);
		scoreText.text = "Score " + ScoreTracker.CurrentScore.ToString();
		timeText.text = "Time " + ((int)(Time.time - GameManager.StartTime)).ToString();
	}
}
