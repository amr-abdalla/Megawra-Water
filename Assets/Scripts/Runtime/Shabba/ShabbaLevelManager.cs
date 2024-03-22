using UnityEngine;
using UnityEngine.SceneManagement;

public class ShabbaLevelManager : MonoBehaviour
{
	public void GoToMainMenu()
	{
		ScoreTracker.TotalScore = 0;
		GlobalReferences.gameManager.Init();
		SceneManager.LoadScene("Main Menu");
	}

	public void GoToNextLevel()
	{
		if (SceneManager.GetActiveScene().name == "ShabbaLevel2")
		{
			SceneManager.LoadScene("Main Menu");
			return;
		}

		SceneManager.LoadScene("ShabbaLevel2");
	}
}
