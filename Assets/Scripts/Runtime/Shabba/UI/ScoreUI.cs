using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textMeshPro;

	void Update()
	{
		textMeshPro.text = ScoreTracker.CurrentScore.ToString(); //of course this is terrible, but I want to sleep
	}
}
