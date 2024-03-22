using System.Collections;
using UnityEngine;

public class WinScreenAudio : MonoBehaviour
{

	[SerializeField] private AudioSource hackyAudio;
	[SerializeField] private AudioClip hackyClip;

	private void OnEnable()
	{
		hackyAudio.Play();
		StartCoroutine(PlayDelayedAudio(8));
	}

	private IEnumerator PlayDelayedAudio(float delay)
	{
		float time = Time.unscaledTime;

		while (Time.unscaledTime - delay < time)
		{
			yield return null;
		}

		hackyAudio.clip = hackyClip;
		hackyAudio.Play();

		yield return null;
	}
}
