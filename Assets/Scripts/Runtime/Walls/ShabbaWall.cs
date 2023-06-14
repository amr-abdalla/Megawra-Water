using UnityEngine;

public class ShabbaWall : MonoBehaviour
{
	private Vector2 playerEnterVelocity;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.TryGetComponent(out RigidbodyMovement player))
		{
			Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
			player.ApplyCancellingForce();
			player.Push(playerEnterVelocity.magnitude, directionToPlayer.normalized);
			
			player.SetMoveDirection(Vector2.Reflect(player.CurrentVelocity.normalized, VectorUtility.GetNormalBetweenTwoPoints(transform.position, player.transform.position)));
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.TryGetComponent(out RigidbodyMovement player))
		{
			playerEnterVelocity = player.CurrentVelocity;
		}
	}

}
