using UnityEngine;

public class ShabbaAudioManager : MonoBehaviour
{
	private int currentDashIndex = 0;
	private int currentCrystalCollectIndex = 0;

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
		collectBigCrystalSource.clip = collectBigCrystalClips[currentCrystalCollectIndex];
		collectBigCrystalSource.Play();
		currentCrystalCollectIndex = Mathf.Min(currentCrystalCollectIndex + 1, collectBigCrystalClips.Length - 1);// (currentCrystalCollectIndex + 1) % collectBigCrystalClips.Length;

	}

}
