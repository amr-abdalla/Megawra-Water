using UnityEngine;
using UnityEngine.InputSystem;

public class ShabbaInput : MonoBehaviour
{
	[SerializeField] private Canvas canvas;
	[SerializeField] private IShabbaMoveAction shabbaMoveAction;

	[SerializeField] private InputActionReference pushAction;
	[SerializeField] private InputActionReference rotateAction;

	[SerializeField] private InputActionReference restartAction;
	[SerializeField] private InputActionReference canvasToggleAction;

	private void InitInput()
	{
		pushAction.action.Enable();
		rotateAction.action.Enable();
		pushAction.action.performed += ctx => shabbaMoveAction.Push(7f);
		rotateAction.action.performed += ctx => shabbaMoveAction.Rotate(ctx.ReadValue<Vector2>());

		restartAction.action.Enable();
		restartAction.action.performed += ctx => RestartScene();
		//rotateAction.action.canceled += ctx => RotateVelocity(0);

		canvasToggleAction.action.Enable();
		canvasToggleAction.action.performed += ctx => ToggleCanvas();
	}

	private void ToggleCanvas()
	{
		canvas.gameObject.SetActive(!canvas.isActiveAndEnabled);
	}

	private void RestartScene()
	{
		// get the current scene name
		string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
		// load the current scene
		UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
	}

	private void Awake()
	{
		shabbaMoveAction = GetComponent<IShabbaMoveAction>();
		InitInput();
	}

}
