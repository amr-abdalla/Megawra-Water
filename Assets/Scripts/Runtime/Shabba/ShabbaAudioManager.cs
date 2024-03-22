using UnityEngine;

public class ShabbaAudioManager : MonoBehaviour
{
	private int currentDashIndex = 0;
	private int currentCrystalCollectIndex = 0;

	private float timeSinceLastPlayedCollectCrystal = -2;

	[SerializeField] private AudioClip[] dashClips;
	[SerializeField] private AudioSource dashAudioSource;


	[SerializeField] private AudioClip[] collectBigCrystalClips;
	[SerializeField] private AudioSource collectBigCrystalSource;

	public void PlayDashClip()
	{
		dashAudioSource.clip = dashClips[currentDashIndex];
		dashAudioSource.Play();
		currentDashIndex = (currentDashIndex + 1) % dashClips.Length;
	}

	public void PlayCollectClip()
	{
		if (Time.time - timeSinceLastPlayedCollectCrystal <= 2)
		{
			if (currentCrystalCollectIndex == 0)
			{
				currentCrystalCollectIndex = 1;
			}
			else
			{
				collectBigCrystalSource.pitch += 0.1f;
			}
		}
		else
		{
			currentCrystalCollectIndex = 0;
			collectBigCrystalSource.pitch = 1;

		}

		timeSinceLastPlayedCollectCrystal = Time.time;

		collectBigCrystalSource.clip = collectBigCrystalClips[currentCrystalCollectIndex];

		collectBigCrystalSource.Play();

	}

}
