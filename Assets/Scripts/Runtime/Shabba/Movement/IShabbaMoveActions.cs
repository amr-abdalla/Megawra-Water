using UnityEngine;

public interface IShabbaMoveAction
{
	Vector2 MoveDirection { get; }
	void Push(float force, Vector2 direction);
	void Rotate(Vector2 direction);
}
