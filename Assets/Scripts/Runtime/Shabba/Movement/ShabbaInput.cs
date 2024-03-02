using UnityEngine;
using UnityEngine.InputSystem;

public class ShabbaInput : MonoBehaviour
{

	[SerializeField] private IShabbaMoveAction shabbaMoveAction;

	[SerializeField] private InputActionReference pushAction;
	[SerializeField] private InputActionReference rotateAction;

	private void InitInput()
	{
		pushAction.action.Enable();
		rotateAction.action.Enable();
		//pushAction.action.performed += ctx => shabbaMoveAction.Push(7f, shabbaMoveAction.MoveDirection);
		//rotateAction.action.performed += ctx => shabbaMoveAction.Rotate(ctx.ReadValue<Vector2>());
		//rotateAction.action.canceled += ctx => RotateVelocity(0);
	}

	private void Awake()
	{
		shabbaMoveAction = GetComponent<IShabbaMoveAction>();
		InitInput();
	}

}
