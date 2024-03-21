using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class restartInput : MonoBehaviour
{

	[SerializeField] InputActionReference restart;

	void Start()
	{
		restart.action.performed += (ctx) => RestartScene();
	}

	public void RestartScene()
	{
		GlobalReferences.gameManager.Init();
		SceneManager.LoadScene(0);
	}
}
