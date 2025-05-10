using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    LevelsConfig levels;

    [SerializeField]
    PlatformManager platformManager;

    [SerializeField]
    private ErabyUIManager erabyUIManager;




    public Action<int> OnNewLevelTransitionEnd;
    public Action<int> OnNewLevelTransitionStart;

    public Action OnGameEnd;
    public void RegisterToLevelStart(Action<int> callback)
    {
        OnNewLevelTransitionEnd += callback;

    }

    public void UnregisterFromLevelStart(Action<int> callback)
    {
        OnNewLevelTransitionEnd -= callback;
    }





    private int level = 0;


    private int numLevels => levels.LevelCount;

    // Start is called before the first frame update
    void Awake()
    {
        level = 0;
        OnNewLevelTransitionStart += HandleLevelStart;
        erabyUIManager.OnEndLevelTransition += () =>
        {
            OnNewLevelTransitionEnd?.Invoke(level);
        };

    }

    void Start()
    {
        StartNextLevel();
    }

    private void HandleLevelStart(int i_level)
    {

        platformManager.ClearPlatforms();
        platformManager.config = levels.LevelConfigs[i_level].ManagerConfig;
        platformManager.InitPlatforms();

    }




    public void StartNextLevel()
    {


        Debug.Log("Starting Level " + level);

        if (level >= numLevels) { OnGameEnd?.Invoke(); }
        else
        {
            OnNewLevelTransitionStart?.Invoke(level);
        }
        level++;


    }




}
