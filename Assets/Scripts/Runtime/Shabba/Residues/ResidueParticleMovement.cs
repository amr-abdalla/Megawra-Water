using System;
using System.Collections;
using UnityEngine;

public class ResidueParticleMovement : MonoBehaviour
{
	private const float _ReturnToStartPositionCheckRateInSeconds = 5f;
	public event Action<float, Vector3> OnGetPushed;
	private Vector3 startPosition;
	public Coroutine currentPush;

	public void Init()
	{
		startPosition = transform.position;
		StartCoroutine(TryReturnToStartPosition());
	}

	private IEnumerator TryReturnToStartPosition()
	{
		while(true)
		{
			if (currentPush == null && transform.position != startPosition)
			{
				currentPush = StartCoroutine(PushRoutine(Vector2.Distance(startPosition, transform.position), (startPosition - transform.position).normalized, 0.5f));
			}

			yield return new WaitForSeconds(_ReturnToStartPositionCheckRateInSeconds);
		}	
	}

	public void GetPushed(float pushValue, Vector2 pushDirection, float speed)
	{
		if (currentPush is not null)
		{
			StopCoroutine(currentPush);
		}

		currentPush = StartCoroutine(PushRoutine(pushValue, pushDirection, speed));
		OnGetPushed?.Invoke(pushValue, pushDirection);
	}

	private IEnumerator PushRoutine(float pushValue, Vector2 pushDirection, float speed)
	{
		Vector2 initialPosition = transform.position;
		Vector2 targetPosition = (Vector2)transform.position + pushDirection * pushValue;
		float totalPushTime = Vector2.Distance(initialPosition, targetPosition) / speed;
		float timeSpent = 0;

		while (Vector2.Distance(transform.position, targetPosition) != 0)
		{
			timeSpent += Time.deltaTime;
			timeSpent = MathF.Min(timeSpent, totalPushTime);
			float t = timeSpent/totalPushTime < 0.5f ? Mathf.Pow(timeSpent / totalPushTime * 2f, 3f) / 2f : 1f - Mathf.Pow((1f - timeSpent / totalPushTime) * 2f, 3f) / 2f;
			transform.position = Vector2.Lerp(initialPosition, targetPosition, t);
			yield return new WaitForEndOfFrame();
		}

		currentPush = null;
	}

}
