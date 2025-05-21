using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SceneTransitionHelper))]
public class EntryMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button storyButton;
    [SerializeField] private Button gameButton;

    [SerializeField] private GameObject creditsUI;
    [SerializeField] private GameObject storyUI;

    private SceneTransitionHelper sceneTransitionHelper;

    private void onCreditsClicked()
    {
        creditsUI.SetActive(true);
    }

    private void onStoryClicked()
    {
        storyUI.SetActive(true);
    }

    private void onGameClicked()
    {
        sceneTransitionHelper.LoadScene("Main Menu");
    }


    void Awake()
    {
        sceneTransitionHelper = GetComponent<SceneTransitionHelper>();
        creditsButton.onClick.AddListener(onCreditsClicked);
        gameButton.onClick.AddListener(onGameClicked);
        storyButton.onClick.AddListener(onStoryClicked);
        gameButton.Select();
    }

    void OnDestroy()
    {
        creditsButton.onClick.RemoveListener(onCreditsClicked);
        gameButton.onClick.RemoveListener(onGameClicked);
        storyButton.onClick.RemoveListener(onStoryClicked);
    }

}
