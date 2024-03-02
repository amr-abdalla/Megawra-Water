using UnityEngine;
using UnityEngine.InputSystem;

public class ShabbaInitialState : ShabbaState
{
	#region Data
	[SerializeField] private float PushForce = 15f;
	#endregion
	[SerializeField] private Rigidbody2D rigidBody;

	private Vector3 MoveDirection;
	private float currentSpeed = 0;
	private float acceleration = 5;
	private float desceleration = 2;
	private float maxSpeed = 10;

	public override Vector3 CurrentDirection => transform.forward;

	public override void Push(float value, Vector3 direction)
	{
		rigidBody.AddForce(direction * value, ForceMode2D.Impulse);
		stateMachine.SetState<ShabbaMoveState>();
	}

	public override void ResetState() { }

	protected override void onStateEnter() { }

	protected override void onStateExit() { }

	protected override void onStateInit() => transform.root.rotation = Quaternion.Euler(0, 0, 180);

	protected override void onStateUpdate() { }

	protected override void onStateFixedUpdate()
	{
		rigidBody.transform.position += Vector3.right * currentSpeed * Time.fixedDeltaTime;

		if (MoveDirection.x == 0)
		{
			currentSpeed = AccelerationUtility.descelerate(currentSpeed, desceleration);
		}
		else
		{
			currentSpeed = AccelerationUtility.accelerate(MoveDirection.x, currentSpeed, maxSpeed, acceleration);
		}
	}

	public override void InitializeControls(Controls i_controls)
	{
		(i_controls as ShabbaControls).RegisterMoveInput(Move);
		(i_controls as ShabbaControls).RegisterPushInput(Push);
	}

	private void Push(InputAction.CallbackContext obj) => Push(PushForce, Vector2.down);

	private void Move(InputAction.CallbackContext obj) => MoveDirection = obj.ReadValue<Vector2>();
}
