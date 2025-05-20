using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public struct LevelEndData
    {
        public int level;
        public bool isSuccess;
        public int score;
        public float time;
        public int remainingWater;
    }

    public struct GameEndData
    {
        public int totalScore;
        public float totalTime;
        public int totalCrashes;
        public bool isSuccess;
    }


    [SerializeField]
    LevelsConfig levels;

    [SerializeField]
    PlatformManager platformManager;

    [SerializeField]
    private ErabyUIManager erabyUIManager;

    [SerializeField]
    private ErabyStateMachineDataProvider erabyStateMachineDataProvider;


    public Action<int> OnNewLevelTransitionEnd;
    public Action<int> OnNewLevelTransitionStart;

    public Action<LevelEndData> OnLevelEnd;

    private GameEndData gameEndData;

    public Action<GameEndData> OnGameEnd;
    public void RegisterToLevelStart(Action<int> callback)
    {
        OnNewLevelTransitionEnd += callback;

    }

    public void UnregisterFromLevelStart(Action<int> callback)
    {
        OnNewLevelTransitionEnd -= callback;
    }




    private int level = 0;

    private float levelStartTime = 0f;


    private int numLevels => levels.LevelCount;

    // Start is called before the first frame update
    void Awake()
    {
        level = 0;
        platformManager.ShouldSpawn = false;
        OnNewLevelTransitionEnd += HandleLevelStart;

        erabyUIManager.OnEndLevelTransition += () =>
        {
            OnNewLevelTransitionEnd?.Invoke(level);
        };

        gameEndData = new GameEndData
        {
            totalScore = 0,
            totalTime = 0,
            totalCrashes = 0,
            isSuccess = false
        };

        erabyUIManager.OnEndGameButtonClicked += ExitGame;
        erabyUIManager.OnNextLevelButtonClicked += HandleNextLevelButtonClicked;

    }

    void Start()
    {
        StartNextLevel();
    }

    public void RestartGame()
    {
        Debug.Log("Restart game");
        level = 0;
        gameEndData = new GameEndData
        {
            totalScore = 0,
            totalTime = 0,
            totalCrashes = 0,
            isSuccess = false
        };
        StartNextLevel();
    }

    void ExitGame()
    {
        // Debug.Log("Exit game");
    }

    void HandleNextLevelButtonClicked()
    {
        // Debug.Log("Next level button clicked");
        StartNextLevel();
    }

    private void HandleLevelStart(int i_level)
    {

        platformManager.ClearPlatforms();
        platformManager.config = levels.LevelConfigs[i_level].ManagerConfig;
        platformManager.InitPlatforms();
        platformManager.ShouldSpawn = true;
        levelStartTime = Time.time;
        erabyStateMachineDataProvider.setNumCrashes(0);

    }

    public void EndLevel(bool isSuccess)
    {
        platformManager.ShouldSpawn = false;
        LevelEndData data = new LevelEndData
        {
            level = level,
            isSuccess = isSuccess,
            score = 0,
            time = Time.time - levelStartTime,
            remainingWater = 5 - erabyStateMachineDataProvider.numCrashes
        };

        gameEndData.totalScore += data.score;
        gameEndData.totalTime += data.time;
        gameEndData.totalCrashes += erabyStateMachineDataProvider.numCrashes;

        gameEndData.isSuccess = isSuccess;

        if (isSuccess)
        {
            Debug.Log("Level " + level + " completed");
        }
        else
        {
            Debug.Log("Level " + level + " failed");
        }

        level++;

        if (level >= numLevels)
        {
            OnGameEnd?.Invoke(gameEndData);
        }
        else
        {
            OnLevelEnd?.Invoke(data);
        }
    }

    public void StartNextLevel()
    {
        Debug.Log("Starting Level " + level);
        OnNewLevelTransitionStart?.Invoke(level);
    }

}
