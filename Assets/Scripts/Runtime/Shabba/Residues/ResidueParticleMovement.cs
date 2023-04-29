using System;
using System.Collections;
using UnityEngine;

public class ResidueParticleMovement : MonoBehaviourBase
{
	[SerializeField] protected AnimationCurve pushEvolutionCurve;

	// Code review : make this a part of a ParticleConfig scriptable object
	private const float _ReturnToStartPositionCheckRateInSeconds = 1f;
	private const float _ReturnToStartPositionSpeed = 0.05f;

	public event Action<float, Vector3> OnGetPushed;
	public event Action<ResidueParticleMovement> OnPushEnded;
	Coroutine currentPush = null;
	Coroutine goBackToStartRoutine = null;

	private Vector3 currentTargetPosition;
	private Vector3 startPosition;

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
	}

	private IEnumerator lerpPosition(float pushValue, Vector2 pushDirection, float speed)
	{
		Vector2 initialPosition = transform.position;
		currentTargetPosition = (Vector2)transform.position + pushDirection * pushValue;
		float totalPushTime = Vector2.Distance(initialPosition, currentTargetPosition) / speed;
		float timeSpent = 0;

		// Code review : need a safer stop condition
		while (timeSpent < totalPushTime)
		{
			float t = pushEvolutionCurve.Evaluate(timeSpent / totalPushTime);
			transform.position = Vector2.Lerp(initialPosition, currentTargetPosition, t);
			timeSpent += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator PushRoutine(float pushValue, Vector2 pushDirection, float speed)
	{
		OnGetPushed?.Invoke(pushValue, pushDirection);
		yield return StartCoroutine(lerpPosition(pushValue, pushDirection, speed));
		OnPushEnded?.Invoke(this);

		goBackToStartRoutine = StartCoroutine(returnToInitialPosition(Vector2.Distance(startPosition, transform.position), (startPosition - transform.position).normalized, _ReturnToStartPositionSpeed));

		this.DisposeCoroutine(ref currentPush);
	}

	private IEnumerator returnToInitialPosition(float pushValue, Vector2 pushDirection, float speed)
    {
		yield return new WaitForSeconds(_ReturnToStartPositionCheckRateInSeconds);
		yield return StartCoroutine(lerpPosition(pushValue, pushDirection, speed));

		this.DisposeCoroutine(ref goBackToStartRoutine);
	}

    #endregion

    private void OnDrawGizmos()
    {
		if (!IsBeingPushed) return;

		Color col = Gizmos.color;
		Gizmos.color = Color.red;

		Vector3 dir = currentTargetPosition - transform.position;

		Gizmos.DrawLine(transform.position, transform.position + dir.normalized * 10f);

		Gizmos.color = col;
	}
}
