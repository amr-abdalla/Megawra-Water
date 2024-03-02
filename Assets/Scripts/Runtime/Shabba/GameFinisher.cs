using TMPro;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
	[SerializeField] GameObject winUI;
	[SerializeField] GameObject scoreUI;
	[SerializeField] TextMeshProUGUI scoreText;
	[SerializeField] TextMeshProUGUI timeText;

	private float StartTime;

	private void Awake()
	{
		Time.timeScale = 1;
		StartTime = Time.time;
		ScoreTracker.CurrentScore = 0;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<StateMachine>(out StateMachine stateMachine))
		{
			Time.timeScale = 0;
			scoreText.text = "Your Score: " + ScoreTracker.CurrentScore;
			timeText.text = "Time Spent: " + (Time.time - StartTime);
			winUI.SetActive(true);
			scoreUI.SetActive(false);
		}
	}
}
