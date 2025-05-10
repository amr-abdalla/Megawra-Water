using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ErabyUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Action OnEndLevelTransition;

    [SerializeField]
    private Image panel;

    [SerializeField]
    private LevelManager levelManager;
    private void Awake()
    {
        levelManager.OnNewLevelTransitionStart += StartLevelTransition;
    }

    private void OnDestroy()
    {
        levelManager.OnNewLevelTransitionStart -= StartLevelTransition;
    }

    private Coroutine transitionRoutine;
    private void StartLevelTransition(int i_level)
    {
        // if (i_level <= 0)
        // {
        //     OnEndLevelTransition?.Invoke();
        //     return;
        // }
        if (transitionRoutine != null) return;
        transitionRoutine = StartCoroutine(StartTransitionRoutine(i_level));
    }

    private IEnumerator StartTransitionRoutine(int i_level)
    {
        Color color = panel.color;
        color.a = 0f;
        panel.color = color;
        float dur = 1f;
        if (i_level < 0)
        {
            for (float t = 0f; t <= dur; t += Time.deltaTime)
            {
                color.a = Mathf.Lerp(0f, 1f, t / dur);
                panel.color = color;

                yield return null;
            }
        }
        else
        {
            color.a = 1f;
            panel.color = color;
        }
        yield return new WaitForSeconds(0.2f);

        dur = 1f;
        for (float t = 0f; t <= dur; t += Time.deltaTime)
        {
            color.a = 1f - Mathf.Lerp(0f, 1f, t / dur);
            panel.color = color;
            yield return null;
        }


        OnEndLevelTransition?.Invoke();
        this.DisposeCoroutine(ref transitionRoutine);
        transitionRoutine = null;

    }
}
