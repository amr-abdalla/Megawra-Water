using System;
using System.Collections;
using UnityEngine;

public class ResidueParticleMovement : MonoBehaviour
{
	public event Action<float, Vector3> OnGetPushed;
	private Vector3 startPosition;
	private float LastPushTime = 0f;

	private void Start()
	{
		startPosition = transform.position;
	}

	private void Update()
	{
		if (Time.time-LastPushTime > 8f && transform.position != startPosition)
		{
			LastPushTime = Time.time;
			StartCoroutine(PushRoutine(Vector2.Distance(startPosition, transform.position), (startPosition - transform.position).normalized, 0.5f));
		}
	}

	public void GetPushed(float pushValue, Vector2 pushDirection)
	{
		OnGetPushed?.Invoke(pushValue, pushDirection);
		LastPushTime = Time.time;
		StartCoroutine(PushRoutine(pushValue, pushDirection));
	}

	private IEnumerator PushRoutine(float pushValue, Vector2 pushDirection, float speed = 2f)
	{
		Vector2 initialPosition = transform.position;
		Vector2 targetPosition = (Vector2)transform.position + pushDirection * pushValue;
		float timeToSpend = Vector2.Distance(initialPosition, targetPosition) / speed;
		float timeSpent = 0;

		while ((Vector2)transform.position != targetPosition)
		{
			timeSpent += Time.deltaTime;
			timeSpent = MathF.Min(timeSpent, timeToSpend);
			float f = timeSpent/timeToSpend < 0.5f ? Mathf.Pow(timeSpent / timeToSpend * 2f, 3f) / 2f : 1f - Mathf.Pow((1f - timeSpent / timeToSpend) * 2f, 3f) / 2f;
			transform.position = Vector2.Lerp(initialPosition, targetPosition, f);
			transform.Translate(pushDirection * pushValue * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}
}
