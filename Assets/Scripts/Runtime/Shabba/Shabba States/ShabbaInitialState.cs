using UnityEngine;
using UnityEngine.InputSystem;

public class ShabbaInitialState : ShabbaState
{
	#region Data
	[SerializeField] private float PushForce = 10;
	[SerializeField] private float acceleration = 5;
	[SerializeField] private float desceleration = 7;
	[SerializeField] private float maxSpeed = 10;
	#endregion
	[SerializeField] private Rigidbody2D rigidBody;
	[SerializeField] private ShabbaAnimationHandler shabbaAnimationHandler;

	private float currentSpeed = 0;
	private Vector3 MoveDirection;

	public override Vector3 CurrentDirection => MoveDirection;

	public override void Push(float value, Vector3 direction)
	{
		rigidBody.AddForce((Vector3.right * currentSpeed / maxSpeed + direction)* value, ForceMode2D.Impulse);
		stateMachine.SetState<ShabbaMoveState>();
	}

	public override void ResetState() { }

	protected override void onStateEnter()
	{
		shabbaAnimationHandler.ResetAnimation();
		rigidBody.velocity = Vector2.zero;
	}

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
		(i_controls as ShabbaControls).RemoveControls();
		(i_controls as ShabbaControls).RegisterMoveInput(Move);
		(i_controls as ShabbaControls).RegisterPushInput(Push);
	}

	private void Push(InputAction.CallbackContext obj) => Push(PushForce, Vector2.down);

	private void Move(InputAction.CallbackContext obj) => MoveDirection = obj.ReadValue<Vector2>();
}
