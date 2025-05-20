using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
	public static TransitionManager instance;

	[SerializeField] private CanvasGroup blackImage;
	[SerializeField] private AnimationCurve fadeCurve;

	private bool _isTransitioning = false;

	public bool IsTransitioning => _isTransitioning;
	
	private const float _defaultFadeDuration = 1f;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	private IEnumerator FadeOut(float duration)
	{
		float currentTime = 0;
		float t;

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			t = currentTime / duration;
			blackImage.alpha = fadeCurve.Evaluate(t);
			yield return null;
		}

		blackImage.alpha = 1;
	}

	private IEnumerator FadeIn(float duration)
	{
		float currentTime = 0;
		float t;

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			t = currentTime / duration;
			blackImage.alpha = fadeCurve.Evaluate(1 - t);
			yield return null;
		}

		blackImage.alpha = 0;
	}

	public IEnumerator TransitionToScene(string loadingScene, string mainScene, float duration = 0)
	{
		_isTransitioning = true;
		if (duration <= 0)
		{
			duration = _defaultFadeDuration;
		}

		yield return FadeOut(duration);

		AsyncOperation loadingOp = SceneManager.LoadSceneAsync(loadingScene, LoadSceneMode.Additive);
		yield return loadingOp;

		yield return FadeIn(duration);

		AsyncOperation levelOp = SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Single);
		yield return new WaitForSeconds(0.5f);
		yield return levelOp;
		_isTransitioning = false;
	}

}
