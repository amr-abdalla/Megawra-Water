using UnityEngine;

public class RigidbodyMovement : MonoBehaviourBase, IShabbaMoveAction
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

	#region Visuals
	[SerializeField] private Transform VisualObject;
	#endregion

	private Rigidbody2D rigidBody;

	#region runtime variables
	private float currentDragTimer = 0f;
	private float currentAngularVelocity = 0f;
	private Vector2 rotateDirection = Vector2.zero;
	private Vector2 moveDirection = Vector2.down;

	#endregion

	#region animation
	[SerializeField] SpriteFrameSwapper dashAnimation;
	[SerializeField] SpriteFrameSwapper idleAnimation;
	[SerializeField] private ShabbaBonesHandler bonesHandler;
	#endregion

	#region UNITY

	void Start()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		resetToInitialState();
		dashAnimation.OnLastFrameReached += OnFinishDashAnimation; // to be removed
	}

	private void OnFinishDashAnimation()
	{
		dashAnimation.Stop();
		idleAnimation.ResetAnimation();
		idleAnimation.Play();
	}

	void OnDisable()
	{
		resetToInitialState();
	}

	void applyRotation()
	{
		var prevRot = transform.rotation;
		transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.up, moveDirection));

		var diff = Vector2.SignedAngle(prevRot * Vector2.up, transform.rotation * Vector2.up);

		var t = Mathf.InverseLerp(-4.39f, 4.39f, diff); // animation
		bonesHandler.SetBones(t); // animation

	}

	private void FixedUpdate()
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

		applyRotation();
	}

	#endregion


	#region PUBLIC API

	public Vector2 MoveDirection => moveDirection;

	[ExposePublicMethod]
	public void Push(float force = 100f) => Push(force, moveDirection);

	public void ApplyCancellingForce() => Push(CurrentVelocity.magnitude, -moveDirection);

	public void SetMoveDirection(Vector2 value) => moveDirection = value;

	public void Push(float force, Vector2 direction)
	{
		rigidBody.drag = initialDrag;

		rigidBody.AddForce(direction * force, ForceMode2D.Impulse);

		idleAnimation.Stop();
		dashAnimation.ResetAnimation();
		dashAnimation.Play();
	}

	[ExposePublicMethod]
	public void ResetToInitialState()
	{
		resetToInitialState();
	}

	[ExposePublicMethod]
	public void Rotate(Vector2 direction)
	{
		rotateDirection = direction;
	}

	public Vector2 CurrentVelocity =>
		null == rigidBody ? MathConstants.VECTOR_2_ZERO : rigidBody.velocity;

	#endregion

	#region PRIVATE

	void onDidStopMoving()
	{
		// Code review : this could trigger an event to tell all listeners
		// that object stopped moving (+ start whataver behaviour should happen in that case)
	}

	void resetToInitialState()
	{
		currentDragTimer = 0f;
		currentAngularVelocity = 0f;

		if (null != rigidBody)
		{
			rigidBody.drag = initialDrag;
			// set object to look down
			rigidBody.transform.rotation = Quaternion.Euler(0, 0, 180);
			rigidBody.velocity = MathConstants.VECTOR_2_ZERO;
		}
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
}
