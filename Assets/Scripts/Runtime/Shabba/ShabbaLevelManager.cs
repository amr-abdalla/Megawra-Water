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

	public void GoToMainMenu()
	{
		ScoreTracker.TotalScore = 0;
		GlobalReferences.gameManager.Init();
		SceneManager.LoadScene("Main Menu");
	}

	public void GoToNextLevel()
	{
		if(SceneManager.GetActiveScene().name == "ShabbaLevel2")
		{
			SceneManager.LoadScene("Main Menu");
		}

		SceneManager.LoadScene("ShabbaLevel2");
	}
}
