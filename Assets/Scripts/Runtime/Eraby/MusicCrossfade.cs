using System.Collections;
using UnityEngine;

public class MusicCrossfade : MonoBehaviour
{

    [SerializeField] [Range(0.0f, 1.0f)] private float volume = 1.0f;

    private AudioSource[] player;
    private IEnumerator[] fader = new IEnumerator[2];
    private int ActivePlayer = 0;

    //Note: If the volumeChangesPerSecond value is higher than the fps, the duration of the fading will be extended!
    private int volumeChangesPerSecond = 15;
    public float fadeDuration = 1.0f;
    private AudioClip activeClip = null;



    #region PUBLIC API

    private void init()
    {
        if (null != player) return;

        //Generate the two AudioSources
        player = new AudioSource[2]{
            gameObject.AddComponent<AudioSource>(),
            gameObject.AddComponent<AudioSource>()
            };

        //Set default values
        foreach (AudioSource s in player)
        {
            s.loop = true;
            s.playOnAwake = false;
            s.volume = 0.0f;
        }
    }

    public float Volume
    {
        get { return volume; }
        set 
        {
            init();
            volume = value;
            player[ActivePlayer].volume = volume;
        }
    }

    public void SetPitch(float i_pitch)
    {
        init();
        foreach (AudioSource s in player)
        {
            s.pitch = i_pitch;
        }
    }

    /// <summary>
    /// Mutes all AudioSources, but does not stop them!
    /// </summary>
    public bool Mute
    {
        get { return player[ActivePlayer].mute; }
        set
        {
            init();
            foreach (AudioSource s in player)
            {
                s.mute = value;
            }
        }
    }

    public void Pause()
    {
        init();
        foreach (AudioSource s in player)
        {
            s.Pause();
        }

    }

    public void UnPause()
    {
        init();
        foreach (AudioSource s in player)
        {
            s.UnPause();
        }
    }

    public AudioClip ActiveClip
    {
        get
        {
            return activeClip;
        }
    }


    /// <summary>
    /// Starts the fading of the provided AudioClip and the running clip
    /// </summary>
    /// <param name="i_clip">AudioClip to fade-in</param>
    public void Play(AudioClip i_clip)
    {
        init();
        //Prevent fading the same clip on both players 
        if (i_clip == player[ActivePlayer].clip)
        {
            return;
        }
        //Kill all playing
        foreach (IEnumerator i in fader)
        {
            if (i != null)
            {
                StopCoroutine(i);
            }
        }

        //Fade-out the active play, if it is not silent (eg: first start)
        if (player[ActivePlayer].volume > 0)
        {
            fader[0] = fadeAudioSource(player[ActivePlayer], fadeDuration, 0.0f, () => { fader[0] = null; });
            StartCoroutine(fader[0]);
        }

        //Fade-in the new clip
        int NextPlayer = (ActivePlayer + 1) % player.Length;
        player[NextPlayer].clip = i_clip;
        activeClip = i_clip;
        player[NextPlayer].Play();
        fader[1] = fadeAudioSource(player[NextPlayer], fadeDuration, Volume, () => { fader[1] = null; });
        StartCoroutine(fader[1]);

        //Register new active player
        ActivePlayer = NextPlayer;
    }

    #endregion

    #region PRIVATE

    /// <summary>
    /// Fades an AudioSource(player) during a given amount of time(duration) to a specific volume(targetVolume)
    /// </summary>
    /// <param name="i_player">AudioSource to be modified</param>
    /// <param name="i_duration">Duration of the fading</param>
    /// <param name="i_targetVolume">Target volume, the player is faded to</param>
    /// <param name="i_finishedCallback">Called when finshed</param>
    /// <returns></returns>
    IEnumerator fadeAudioSource(AudioSource i_player, float i_duration, float i_targetVolume, System.Action i_finishedCallback)
    {
        //Calculate the steps
        int Steps = (int)(volumeChangesPerSecond * i_duration);
        float StepTime = i_duration / Steps;
        float StepSize = (i_targetVolume - i_player.volume) / Steps;

        //Fade now
        for (int i = 1; i < Steps; i++)
        {
            i_player.volume += StepSize;
            yield return new WaitForSeconds(StepTime);
        }
        //Make sure the targetVolume is set
        i_player.volume = i_targetVolume;

        //Callback
        if (i_finishedCallback != null)
        {
            i_finishedCallback();
        }
    }

    #endregion
}


