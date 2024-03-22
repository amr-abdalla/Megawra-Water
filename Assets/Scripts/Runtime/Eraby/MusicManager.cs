using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip[] audioclips;
    [SerializeField]  private PhysicsBody2D erabyBody;
    [SerializeField] private MusicCrossfade crossfade;

    private float minVelocity = 0f;
    [SerializeField] private float maxVelocity = 100f;

    int selectedIndex = 0;
    Coroutine playClipRoutine = null;


    private void Awake()
    {
        crossfade.Play(audioclips[selectedIndex]);
    }

    void Update()
    {
        int index = getSelectedIndex();

        if(selectedIndex != index)
        {
            selectedIndex = index;
            this.DisposeCoroutine(ref playClipRoutine);
            playClipRoutine = StartCoroutine(playWithDelay());
        }
    }

    IEnumerator playWithDelay()
    {
        yield return new WaitForSeconds(1.5f);
        crossfade.Play(audioclips[selectedIndex]);

        this.DisposeCoroutine(ref playClipRoutine);
    }

    int getSelectedIndex()
    {
        float velocity = Mathf.Abs(erabyBody.VelocityX);
        float t = Mathf.InverseLerp(minVelocity, maxVelocity, velocity);

        float indexF = Mathf.Lerp(0, audioclips.Length - 1, t);
        int index = Mathf.RoundToInt(indexF);
        index = Mathf.Clamp(index, 0, audioclips.Length - 1);

        return index;
    }
}
