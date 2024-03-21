using UnityEngine;

public class GlobalReferences : MonoBehaviour
{
	private static GameManager cachedGameManager;

	public static GameManager gameManager
	{
		get
		{
			if (cachedGameManager is null)
			{
				InitializeGameManager();
			}

			return cachedGameManager;
		}
	}

	private static void InitializeGameManager()
	{
		cachedGameManager = FindObjectOfType<GameManager>();
	}
}
