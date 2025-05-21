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

    private SceneTransitionHelper sceneTransitionHelper;

    private void onCreditsClicked()
    {
        creditsUI.SetActive(true);
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
        gameButton.Select();
    }

    void OnDestroy()
    {
        creditsButton.onClick.RemoveListener(onCreditsClicked);
        gameButton.onClick.RemoveListener(onGameClicked);
    }

}
