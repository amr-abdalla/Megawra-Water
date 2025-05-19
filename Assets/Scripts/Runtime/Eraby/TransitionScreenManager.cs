using UnityEngine;
using UnityEngine.UI;

public class TransitionScreenManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image background;
    [SerializeField] private Image[] panels = new Image[6];

    [SerializeField] private float timeToBeat = 180f;


    public void ShowTransitionScreen(LevelManager.LevelEndData data)
    {
        background.gameObject.SetActive(true);
        for (int i = 0; i < panels.Length; i++)
            panels[i].gameObject.SetActive(false);

        int panelIndex = GetPanelIndex(data);
        if (panelIndex >= 0 && panelIndex < panels.Length)
            panels[panelIndex].gameObject.SetActive(true);
    }

    private int GetPanelIndex(LevelManager.LevelEndData data)
    {
        float index = data.remainingWater - 1;
        if (timeToBeat > data.time) index++;
        Debug.Log($"Index: {index}, Remaining Water: {data.remainingWater}, Time: {data.time}");
        return Mathf.Clamp((int)index, 0, panels.Length - 1);
    }

    public void HideTransitionScreen()
    {
        background.gameObject.SetActive(false);
        foreach (var panel in panels)
            panel.gameObject.SetActive(false);
    }
}
