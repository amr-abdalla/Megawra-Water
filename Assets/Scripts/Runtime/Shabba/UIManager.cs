using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] GameObject loseUI;
	[SerializeField] FinalScoreUI winUI;

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
		winUI.gameObject.SetActive(true);
		winUI.Setup();
	}
}
