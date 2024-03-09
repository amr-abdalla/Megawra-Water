using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class restartInput : MonoBehaviour
{

	[SerializeField] InputActionReference restart;

	void Start()
	{
		restart.action.performed += (ctx) => SceneManager.LoadScene(0);
	}

}
