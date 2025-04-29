using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject loseUI;
	[SerializeField] FinalScoreUI winUI;

	private void Start()
	{
		GameStateHandler.Instance.OnLoseAction += OnLose;
		GameStateHandler.Instance.OnWinAction += OnWin;
	}

	private void OnDestroy()
	{
		GameStateHandler.Instance.OnLoseAction -= OnLose;
		GameStateHandler.Instance.OnWinAction -= OnWin;
	}

	private void OnLose()
	{
		loseUI.SetActive(true);
	}

	private void OnWin()
	{
		winUI.gameObject.SetActive(true);
		winUI.Setup();
	}
}
