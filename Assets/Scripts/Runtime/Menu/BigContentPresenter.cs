using UnityEngine;
using UnityEngine.InputSystem;

public class BigContentPresenter : MonoBehaviour
{
	[SerializeField] private RectTransform content;
	[SerializeField] private float baseSpeed = 200f;
	[SerializeField] private InputActionReference navigationAction;

	private float targetPositionY;
	private float currentSpeed;
	private float maxSpeed;
	private float minSpeed;

	void Start()
	{
		currentSpeed = baseSpeed;
		maxSpeed = baseSpeed * 2.5f;
		minSpeed = 0f;
		float screenHeight = ((RectTransform)content.parent).rect.height;
		targetPositionY = content.rect.height + screenHeight + content.transform.position.y;
	}

	private void OnEnable()
	{
		navigationAction.action.Enable();
	}

	private void OnDisable()
	{
		navigationAction.action.Disable();
	}

	void Update()
	{
		Vector2 navInput = navigationAction.action.ReadValue<Vector2>();

		if (navInput.y == -1)
		{
			currentSpeed = maxSpeed;
		}
		else if (navInput.y == 1)
		{
			currentSpeed = minSpeed;
		}
		else
		{
			currentSpeed = baseSpeed;
		}

		if (content.anchoredPosition.y >= targetPositionY)
		{
			//TODO: idk, exit the UI or something
			Debug.Log("Done");
			return;
		}

		content.anchoredPosition += Vector2.up * currentSpeed * Time.deltaTime;
	}
}
