using UnityEngine;
using UnityEngine.SceneManagement;

public class ShabbaLevelManager
{
	private const int MainMenuBuildIndex = 0;
	private const string ShabbaSceneKeyword = "Shabba";

	public void GoToMainMenu() => SceneManager.LoadScene(MainMenuBuildIndex);

	public bool IsCurrentLevelTheFinalLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int totalSceneCount = SceneManager.sceneCountInBuildSettings;

		return currentSceneIndex >= totalSceneCount - 1;
	}

	public void GoToNextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int totalSceneCount = SceneManager.sceneCountInBuildSettings;

		if (currentSceneIndex >= totalSceneCount - 1)
		{
			SceneManager.LoadScene(MainMenuBuildIndex);
			return;
		}

		string nextScenePath = SceneUtility.GetScenePathByBuildIndex(currentSceneIndex + 1);
		string nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextScenePath);

		if (nextSceneName.Contains(ShabbaSceneKeyword))
		{
			GameStateHandler.Instance.StartCoroutine(TransitionManager.instance.TransitionToScene("ShabbaLoadingScene", nextSceneName));
		}
		else
		{
			Debug.LogError($"Failed to Go To Next Level, current scene index is {currentSceneIndex} and next scene name is {nextSceneName} (it should contain the word Shabba)");
		}
	}

	public void GoToFirstLevel() => SceneManager.LoadScene("ShabbaLevel1");
}
