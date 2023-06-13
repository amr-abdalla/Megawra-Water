using UnityEngine;

public class Border : MonoBehaviour
{
	public enum Axis
	{
		Horizontal,
		Vertical
	}

	public float PushIncreaseRate = 0.1f;
	public float MaxPushForce = 5f;
	private float currentPushForce = 0f;
	private Vector2 playerEnterVelocity;

	public Vector2 normal;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.TryGetComponent(out RigidbodyMovement player))
		{
			Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
			player.Push(player.CurrentVelocity.magnitude, -player.moveDirection);
			player.Push(player.CurrentVelocity.magnitude, directionToPlayer);
			player.moveDirection = Vector2.Reflect(player.moveDirection, normal);

		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.TryGetComponent(out RigidbodyMovement player))
		{
			playerEnterVelocity = player.CurrentVelocity;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		//if (collision.TryGetComponent(out RigidbodyMovement player))
		//{
		//	Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;

		//	if (Vector2.Dot(directionToPlayer.normalized, player.CurrentVelocity.normalized) < 0)
		//	{
		//		player.Push(currentPushForce, directionToPlayer);
		//		currentPushForce += PushIncreaseRate * Time.deltaTime;
		//		currentPushForce = Mathf.Clamp(currentPushForce, 0, MaxPushForce);
		//	}

		//}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out RigidbodyMovement player))
		{
			currentPushForce = 0f;
		}
	}
}
