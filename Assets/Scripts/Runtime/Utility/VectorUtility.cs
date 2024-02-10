using System.Collections.Generic;
using UnityEngine;

public static class VectorUtility
{
	public static Vector3 GetClosestVector(this Vector3 origin, IEnumerable<Vector3> vectors)
	{
		Vector3 closestVector = Vector3.zero;
		float currentClosestDistance = Mathf.Infinity;

		foreach(Vector3 vector in vectors)
		{
			float distance = Vector2.Distance(vector, origin);
			if (distance < currentClosestDistance)
			{
				currentClosestDistance = distance;
				closestVector = vector;
			}
		}

		return closestVector;
	}
}
