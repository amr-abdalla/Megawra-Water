using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryMenuBackgroundColorSwitcher : MonoBehaviour
{
	[SerializeField] Image image;

	List<Color> targetColors = new();

	int currentTargetColor = 0;
	float t = 0;
	[SerializeField] float speed = 10;
	[SerializeField] AnimationCurve animationCurve;

	void Start()
	{
		Color color1 = new Color(0f, 50 / 255f, 108 / 255f);
		Color color2 = new Color(0f, 181 / 255f, 181 / 255f);
		targetColors.Add(color2);
		targetColors.Add(color1);
	}


	void Update()
	{
		if (t >= 1)
		{
			currentTargetColor++;
			currentTargetColor %= 2;
			t = 0;
		}

		t += Time.deltaTime * speed;
		int prev = currentTargetColor - 1;

		if (prev == -1) prev = 1;

		image.color = Color.Lerp(targetColors[prev], targetColors[currentTargetColor], animationCurve.Evaluate(t));

	}
}
