using UnityEngine;

public class ShabbaWall : MonoBehaviour
{
	private Vector2 playerEnterVelocity;
	private Vector2 pushDirection;
	private Vector2 normal;
	private Vector2 playerVelocity;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("col");
		if (collision.transform.TryGetComponent(out RigidbodyMovement player))
		{
			Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
			player.ApplyCancellingForce();

			normal = transform.position.x > collision.transform.position.x ? Vector2.left : Vector2.right ;
			playerVelocity = -player.MoveDirection;
			pushDirection = -Vector2.Reflect(playerVelocity, normal).normalized;

			player.SetMoveDirection(pushDirection);
			player.Push(playerEnterVelocity.magnitude, pushDirection);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("trig");
		if (collision.transform.TryGetComponent(out RigidbodyMovement player))
		{
			playerEnterVelocity = player.CurrentVelocity;
		}
	}

	//	private void OnDrawGizmos()
	//	{
	//		Color col = Gizmos.color;

	//		Gizmos.color = Color.red;

	//		Gizmos.DrawLine(transform.position, (Vector2)transform.position + pushDirection.normalized * 10f);

	//		Gizmos.color = Color.blue;

	//		Gizmos.DrawLine(transform.position, (Vector2)transform.position + normal.normalized * 10f);

	//		Gizmos.color = Color.green;

	//		Gizmos.DrawLine(transform.position, (Vector2)transform.position + playerVelocity.normalized * 10f);


	//		Gizmos.color = col;
	//	}
}
