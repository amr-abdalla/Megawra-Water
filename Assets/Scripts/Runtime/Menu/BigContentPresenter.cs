using UnityEngine;
using UnityEngine.InputSystem;


public class BigContentPresenter : MonoBehaviour
{
	[SerializeField] private RectTransform content;
	[SerializeField] private float baseSpeed = 200f;
	[SerializeField] private InputActionReference navigationAction;
	[SerializeField] private InputActionReference exitToMenuAction;
	[SerializeField] private GameObject contentUI;


	private float targetPositionY;
	private float currentSpeed;
	private float maxSpeed;
	private float minSpeed;
	private Vector3 initialPosition;

	private bool isDone = false;


	void ResetContent()
	{

		isDone = false;
		content.transform.position = initialPosition;
		currentSpeed = baseSpeed;
		maxSpeed = baseSpeed * 5f;
		minSpeed = 0f;
		float screenHeight = ((RectTransform)content.parent).rect.height;
		targetPositionY = content.rect.height + screenHeight + content.transform.position.y;
	}

	void Start()
	{
		initialPosition = content.transform.position;
		ResetContent();
	}

	private void OnEnable()
	{
		exitToMenuAction.action.Enable();
		exitToMenuAction.action.performed += exitToMenuActionHandler;
	}
	private void OnDisable()
	{
		exitToMenuAction.action.Disable();
		exitToMenuAction.action.performed -= exitToMenuActionHandler;
	}
	private void ExitToMenu()
	{
		ResetContent();
		contentUI.SetActive(false);
	}

	private void exitToMenuActionHandler(InputAction.CallbackContext obj)
	{
		ExitToMenu();
	}

	void Update()
	{
		if (isDone)
		{
			return;
		}

		Vector2 navInput = navigationAction.action.ReadValue<Vector2>();

		if (navInput.y < 0)
		{
			currentSpeed = maxSpeed;
		}
		else if (navInput.y > 0)
		{
			currentSpeed = minSpeed;
		}
		else
		{
			currentSpeed = baseSpeed;
		}

		if (content.anchoredPosition.y >= targetPositionY)
		{
			isDone = true;
			ExitToMenu();
			return;
		}

		content.anchoredPosition += Vector2.up * currentSpeed * Time.deltaTime;
	}
}
