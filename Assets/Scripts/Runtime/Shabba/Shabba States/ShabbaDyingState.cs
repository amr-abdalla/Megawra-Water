using UnityEngine;

public class ShabbaDyingState : ShabbaState
{
	private Vector2 moveDirection = Vector2.down;

	public override Vector3 CurrentDirection => moveDirection;

	[SerializeField] private Rigidbody2D rigidBody;
	[SerializeField] private ShabbaAnimationHandler shabbaAnimationHandler;

	private float StartTime;

	private const float DeathTime = 4f;

	public override void Push(float value, Vector3 direction)
	{
		rigidBody.AddForce(CurrentDirection * value, ForceMode2D.Impulse);
		stateMachine.SetState<ShabbaMoveState>();
	}

	public override void ResetState() { }

	protected override void onStateEnter()
	{
		shabbaAnimationHandler.ResetAnimation();
		StartTime = Time.time;
	}

	protected override void onStateExit() 
	{
		isInit = false; //hack
	}

	protected override void onStateInit() { }

	protected override void onStateFixedUpdate()
	{
		rigidBody.transform.position = (Vector2)rigidBody.transform.position + Vector2.up * Time.fixedDeltaTime * 2f;

		if (Time.time - StartTime > DeathTime)
		{
			GlobalReferences.gameManager.OnLose();
		}
	}

	public override void InitializeControls(Controls i_controls)
	{
		base.InitializeControls(i_controls);
		i_controls.RemoveControls();
	}

	protected override void onStateUpdate() {}
}
