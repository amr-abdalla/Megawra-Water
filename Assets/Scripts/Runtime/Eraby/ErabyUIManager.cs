using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ErabyUIManager : MonoBehaviourBase
{
    // Start is called before the first frame update
    public Action OnEndLevelTransition;

    public Action OnNextLevelButtonClicked;
    public Action OnEndGameButtonClicked;

    [SerializeField]
    private Image panel;


    [SerializeField]
    private Image levelFailPanel;

    [SerializeField]
    private Image gameSuccessPanel;

    [SerializeField]
    private Button nextLevelButton;

    [SerializeField]
    private Button endGameButton;

    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private TransitionScreenManager transitionScreenManager;

    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Button restartWinButton;

    private TransitionScreenManager levelSuccessPanel => transitionScreenManager;

    [SerializeField]
    private SceneTransitionHelper sceneTransitionHelper;

    private void handleNextLevelButtonClicked()
    {
        OnNextLevelButtonClicked?.Invoke();
    }

    private void handleEndGameButtonClicked()
    {
        OnEndGameButtonClicked?.Invoke();
        sceneTransitionHelper.LoadScene("Entry Scene");
    }

    private void handleRestartButtonClicked()
    {
        Debug.Log("Restarting game");
        levelManager.RestartGame();
    }
    protected override void Awake()
    {
        base.Awake();
        levelManager.OnNewLevelTransitionStart += StartLevelTransition;
        levelManager.OnLevelEnd += HandleLevelEnd;
        levelManager.OnGameEnd += HandleGameEnd;
        nextLevelButton.onClick.AddListener(handleNextLevelButtonClicked);
        endGameButton.onClick.AddListener(handleEndGameButtonClicked);
        restartButton.onClick.AddListener(handleRestartButtonClicked);
        restartWinButton.onClick.AddListener(handleRestartButtonClicked);
    }

    private void OnDestroy()
    {
        levelManager.OnNewLevelTransitionStart -= StartLevelTransition;
        levelManager.OnLevelEnd -= HandleLevelEnd;
        levelManager.OnGameEnd -= HandleGameEnd;
        nextLevelButton.onClick.RemoveListener(handleNextLevelButtonClicked);
        endGameButton.onClick.RemoveListener(handleEndGameButtonClicked);
        restartButton.onClick.RemoveListener(handleRestartButtonClicked);
        restartWinButton.onClick.RemoveListener(handleRestartButtonClicked);
    }

    private void showButtons(bool show = true, bool restart = false, bool gameEnd = false)
    {
        nextLevelButton.gameObject.SetActive(show && !restart);
        endGameButton.gameObject.SetActive(show);
        restartButton.gameObject.SetActive(show && restart && !gameEnd);
        restartWinButton.gameObject.SetActive(show && restart && gameEnd);

        if (!show) return;

        if (restart)
            (gameEnd ? (Selectable)restartWinButton : restartButton).Select();
        else
            nextLevelButton.Select();
    }

    private void HandleLevelEnd(LevelManager.LevelEndData levelEndData)
    {
        if (levelEndData.isSuccess)
        {
            ShowLevelSuccessPanel(levelEndData);
            Debug.Log("Level Success");
            Debug.Log($"Level: {levelEndData.level}, Score: {levelEndData.score}, Time: {levelEndData.time}, Remaining Water: {levelEndData.remainingWater}");
        }
        else
        {
            ShowLevelFailPanel();
        }
    }

    private void HandleGameEnd(LevelManager.GameEndData gameEndData)
    {
        if (gameEndData.isSuccess)
        {
            ShowGameSuccessPanel();
        }
        else
        {
            ShowLevelFailPanel();
        }
        Debug.Log("Game Success");
        Debug.Log($"Total Score: {gameEndData.totalScore}, Total Time: {gameEndData.totalTime}, Total Crashes: {gameEndData.totalCrashes}");
    }



    public void ShowLevelSuccessPanel(LevelManager.LevelEndData levelEndData)
    {
        transitionScreenManager.gameObject.SetActive(true);
        transitionScreenManager.ShowTransitionScreen(levelEndData);
        levelFailPanel.gameObject.SetActive(false);
        gameSuccessPanel.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        showButtons();
    }

    [ExposePublicMethod]
    public void ShowLevelFailPanel()
    {
        levelSuccessPanel.gameObject.SetActive(false);
        levelFailPanel.gameObject.SetActive(true);
        gameSuccessPanel.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        showButtons(true, true);
    }

    public void ShowGameSuccessPanel()
    {
        levelSuccessPanel.gameObject.SetActive(false);
        levelFailPanel.gameObject.SetActive(false);
        gameSuccessPanel.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        endGameButton.gameObject.SetActive(true);
        endGameButton.Select();
        restartWinButton.gameObject.SetActive(true);
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

    private float easeInExpo(float x)
    {
        return x <= 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
    }

    private IEnumerator StartTransitionRoutine(int i_level)
    {
        levelFailPanel.gameObject.SetActive(false);
        levelSuccessPanel.gameObject.SetActive(false);
        gameSuccessPanel.gameObject.SetActive(false);
        showButtons(false);
        panel.gameObject.SetActive(true);
        Color color = panel.color;
        color.a = 1f;
        panel.color = color;
        // float dur = 1f;
        // if (i_level > 0)
        // {
        //     for (float t = 0f; t <= dur; t += Time.deltaTime)
        //     {
        //         color.a = Mathf.Lerp(0f, 1f, t / dur);
        //         panel.color = color;

        //         yield return null;
        //     }
        // }
        // else
        // {
        //     panel.gameObject.SetActive(true);
        //     color.a = 1f;
        //     panel.color = color;
        // }
        yield return new WaitForSeconds(0.2f);

        float dur = 2f;
        for (float t = 0f; t <= dur; t += Time.deltaTime)
        {
            color.a = 1f - easeInExpo(t / dur);
            panel.color = color;
            yield return null;
        }
        OnEndLevelTransition?.Invoke();
        this.DisposeCoroutine(ref transitionRoutine);
        transitionRoutine = null;

    }
}
