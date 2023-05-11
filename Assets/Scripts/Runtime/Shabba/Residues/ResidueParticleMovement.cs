using System;
using System.Collections;
using UnityEngine;

public class ResidueParticleMovement : MonoBehaviourBase
{
	[SerializeField] protected AnimationCurve pushEvolutionCurve;
	[SerializeField] private ResidueParticlePhysicsConfig residueParticlePhysicsConfig;

	public event Action<float, Vector3> OnGetPushed;
	public event Action<ResidueParticleMovement> OnPushEnded;
	Coroutine currentPush = null;
	Coroutine goBackToStartRoutine = null;
	Coroutine lerpRoutine = null;

	private Vector2 currentTargetPosition;
	private Vector2 startPosition;

	#region PUBLIC API
	public void Init()
	{
		startPosition = transform.position;
	}

	public void GetPushed(float pushValue, Vector2 pushDirection, float speed)
	{
		stopAllMovement();
		currentPush = StartCoroutine(PushRoutine(pushValue, pushDirection, speed));
	}

	public bool IsBeingPushed => null != currentPush;

	public bool IsReturningToStart => null != goBackToStartRoutine;

	public bool IsMoving => IsBeingPushed || IsReturningToStart;

	#endregion

	#region PRIVATE

	void stopAllMovement()
    {
		this.DisposeCoroutine(ref currentPush);
		this.DisposeCoroutine(ref goBackToStartRoutine);
		this.DisposeCoroutine(ref lerpRoutine);
	}

	private IEnumerator lerpPosition(float pushValue, Vector2 pushDirection, float speed)
	{
		Vector2 initialPosition = transform.position;
		currentTargetPosition = (Vector2)transform.position + pushDirection * pushValue;
		float totalPushTime = Vector2.Distance(initialPosition, currentTargetPosition) / speed;
		float timeSpent = 0;

		while (timeSpent <= totalPushTime)
		{
			float t = pushEvolutionCurve.Evaluate(timeSpent / totalPushTime);
			transform.position = Vector2.Lerp(initialPosition, currentTargetPosition, t);
			timeSpent += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}

		transform.position = currentTargetPosition;
	}

	private IEnumerator PushRoutine(float pushValue, Vector2 pushDirection, float speed)
	{
		OnGetPushed?.Invoke(pushValue, pushDirection);

		yield return StartCoroutine(StartLerp(pushValue, pushDirection, speed));

		OnPushEnded?.Invoke(this);
		goBackToStartRoutine = StartCoroutine(returnToInitialPosition(Vector2.Distance(startPosition, transform.position), (startPosition - (Vector2)transform.position).normalized, residueParticlePhysicsConfig.ReturnToStartPositionSpeed));

		this.DisposeCoroutine(ref currentPush);
	}

	private IEnumerator returnToInitialPosition(float pushValue, Vector2 pushDirection, float speed)
	{
		yield return new WaitForSeconds(residueParticlePhysicsConfig.ReturnToStartPositionDelayInSeconds);
		yield return StartCoroutine(StartLerp(pushValue, pushDirection, speed));

		this.DisposeCoroutine(ref goBackToStartRoutine);
	}

	private IEnumerator StartLerp(float pushValue, Vector2 pushDirection, float speed)
	{
		this.DisposeCoroutine(ref lerpRoutine);

		yield return lerpRoutine = StartCoroutine(lerpPosition(pushValue, pushDirection, speed));

		this.DisposeCoroutine(ref lerpRoutine);
	}

    #endregion

    private void OnDrawGizmos()
    {
		if (!IsMoving) return;

		Color col = Gizmos.color;

		if(IsBeingPushed)
		{
			Gizmos.color = Color.red;
		}
		else if (IsReturningToStart)
		{
			Gizmos.color = Color.blue;
		}

		Vector2 dir = currentTargetPosition - (Vector2)transform.position;

		Gizmos.DrawLine(transform.position, (Vector2)transform.position + dir.normalized * 10f * Vector2.Distance(transform.position, currentTargetPosition));

		Gizmos.color = col;
	}
}
