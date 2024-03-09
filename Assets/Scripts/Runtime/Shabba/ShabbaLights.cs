using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShabbaLights : MonoBehaviour
{
	private float startY = 9.83f;
	private float finishY = -35f;
	[SerializeField] private GameObject shabba;
	[SerializeField] private Light2D light2D;

	void Update()
	{
		var currentY = Mathf.InverseLerp(startY, finishY, shabba.transform.position.y);
		light2D.intensity = Mathf.Lerp(2.6f, 0, currentY);
		Debug.Log(light2D.intensity);
	}
}
