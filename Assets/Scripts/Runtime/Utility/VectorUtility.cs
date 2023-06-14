using UnityEngine;

public static class VectorUtility
{
	public static Vector2 GetNormalBetweenTwoPoints(Vector2 point1, Vector2 point2) => new Vector2(point1.y - point2.y, point2.x - point1.x).normalized;

}
