using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShabbaMoveState : ShabbaState
{
	#region Data
	[SerializeField] private AngularAccelerationData angularAccelerationData;
	[SerializeField] private DragSettingsData dragSettingsData;

	[Header("Max values")] [SerializeField] private float minSpeed = 1f;
	[SerializeField] private float maxSpeed = 10f;
	[SerializeField] private float maxDrag = 10f;
	[SerializeField] private float timeToMaxDrag = 1f;
	[Header("Initial values")] [SerializeField] private float initialDrag = 0f;
	#endregion

	#region runtime variables
	private float currentDragTimer = 0f;
	private float currentAngularVelocity = 0f;
	private Vector2 rotateDirection = Vector2.zero;
	private Vector2 moveDirection = Vector2.down;
	#endregion

	[SerializeField] private Rigidbody2D rigidBody;
	[SerializeField] private ShabbaAnimationHandler shabbaAnimationHandler;
	[SerializeField] private GameObject loseScreen;
	[SerializeField] private ShabbaAudioManager audioSource;

	#region events
	public void onDidStopMoving() 
	{
		stateMachine.SetState<ShabbaDyingState>();
	}// => stateMachine.SetState<>();

	public Action OnPush;
	public Action<float> OnRotate;

	#endregion

	#region State Methods

	public override void ResetState() { } //not sure if needed

	protected override void onStateEnter()
	{
		shabbaAnimationHandler.PlayIdleAnimation();
		resetVariableToInitialValues();
		OnPush += shabbaAnimationHandler.InvokeDashAnimation;
		OnRotate += shabbaAnimationHandler.OnRotate;
	}

	protected override void onStateExit()
	{
		resetVariableToInitialValues();
		OnPush -= shabbaAnimationHandler.InvokeDashAnimation;
		OnRotate -= shabbaAnimationHandler.OnRotate;
		isInit = false; //hack
	}

	protected override void onStateInit() { } //not sure if needed

	protected override void onStateUpdate() { } //not needed

	protected override void onStateFixedUpdate()
	{
		float deltaAngle = ComputeAngularVelocity(rotateDirection.x) * Time.deltaTime;
		moveDirection = Quaternion.Euler(0, 0, -deltaAngle) * moveDirection;

		rigidBody.velocity = moveDirection * rigidBody.velocity.magnitude;

		rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);

		// set the direction of the movement to be forward

		// if velocity is minimum, set linear drag to initial drag
		if (rigidBody.velocity.magnitude >= minSpeed)
		{
			applyDrag();
		}
		else
		{
			onDidStopMoving();
		}

		ApplyRotation();
	}

	#endregion

	#region PRIVATE

	private void resetVariableToInitialValues()
	{
		currentDragTimer = 0f;
		currentAngularVelocity = 0f;

		if (null != rigidBody)
		{
			rigidBody.drag = initialDrag;
			moveDirection = rigidBody.velocity.normalized;
			// set object to look down
			//rigidBody.transform.rotation = Quaternion.Euler(0, 0, 180);
		}
	}

	void ApplyRotation()
	{
		var prevRot = rigidBody.transform.rotation;
		rigidBody.transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, moveDirection));

		var diff = Vector2.SignedAngle(prevRot * Vector2.up, rigidBody.transform.rotation * Vector2.up);

		OnRotate?.Invoke(diff);
	}

	void applyDrag()
	{
		float tDrag = currentDragTimer / timeToMaxDrag;
		float tEval = dragSettingsData.dragEvolutionCurve.Evaluate(tDrag);
		rigidBody.drag = Mathf.Lerp(rigidBody.drag, maxDrag, tEval);

		currentDragTimer += Time.fixedDeltaTime;
		currentDragTimer = Mathf.Clamp(currentDragTimer, 0f, timeToMaxDrag);
	}

	private float ComputeAngularVelocity(float i_angleSign)
	{
		if (i_angleSign == 0f)
			currentAngularVelocity = AccelerationUtility.descelerate(
				currentAngularVelocity,
				angularAccelerationData.angularDesceleration
			);
		else
			currentAngularVelocity = AccelerationUtility.accelerate(
				i_angleSign,
				currentAngularVelocity,
				angularAccelerationData.maxAngularVelocity,
				angularAccelerationData.angularAcceleration
			);

		return currentAngularVelocity;
	}

	#endregion

	public override void InitializeControls(Controls i_controls)
	{
		i_controls.RemoveControls();
		(i_controls as ShabbaControls).RegisterMoveInput(Move);
	}

	#region Input Functions
	private void Move(InputAction.CallbackContext ctx)
	{
		rotateDirection = ctx.ReadValue<Vector2>();
	}

	private void Push(InputAction.CallbackContext ctx)
	{
		Push(7f, moveDirection);
	}

	public override void Push(float value, Vector3 direction)
	{
		rigidBody.drag = initialDrag;

		rigidBody.AddForce(direction * value, ForceMode2D.Impulse);

		audioSource.PlayDashClip();

		OnPush?.Invoke();
	}

	public override Vector3 CurrentDirection => moveDirection;

	#endregion

}
