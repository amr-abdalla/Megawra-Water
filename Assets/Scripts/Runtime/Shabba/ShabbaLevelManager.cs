using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ShabbaLevelManager : MonoBehaviour
{
	[SerializeField] InputActionReference restart;

	void Start()
	{
		restart.action.performed += (ctx) => GoToNextLevel();
	}

	public void RestartScene()
	{
		ScoreTracker.TotalScore = 0;
		GlobalReferences.gameManager.Init();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void GoToNextLevel()
	{
		if(SceneManager.GetActiveScene().buildIndex == 1)
		{
			return;
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
