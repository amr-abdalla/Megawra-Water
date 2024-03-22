using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    LevelsConfig levels;

    [SerializeField]
    PlatformManager platformManager;


    private Action<int> OnLevelStart;

    public Action OnGameEnd;
    public void RegisterToLevelStart(Action<int> callback)
    {
        OnLevelStart += callback;

    }

    public void UnregisterFromLevelStart(Action<int> callback)
    {
        OnLevelStart -= callback;
    }





    private int level = 0;


    private int numLevels => levels.LevelCount;

    // Start is called before the first frame update
    void Awake()
    {
        level = 0;
        OnLevelStart += HandleLevelStart;

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
            OnLevelStart?.Invoke(level);
        }
        level++;


    }




}
