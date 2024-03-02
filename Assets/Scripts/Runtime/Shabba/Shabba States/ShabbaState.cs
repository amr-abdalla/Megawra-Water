using UnityEngine;

public abstract class ShabbaState : State
{
	public abstract void Push(float value, Vector3 direction);

	public abstract Vector3 CurrentDirection { get; }
}
