using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErabyWaterBar : MonoBehaviour
{
    [SerializeField]
    private ErabyStateMachineDataProvider dataProvider;


    [SerializeField]
    private List<Image> waterBarImages = new List<Image>();

    [SerializeField]
    private void Start()
    {

        dataProvider.OnNumCrashesChanged += UpdateWaterBar;
    }

    private void UpdateWaterBar(int numCrashes)
    {
        for (int i = 0; i < waterBarImages.Count; i++)
        {
            if (i < numCrashes)
            {
                waterBarImages[i].gameObject.SetActive(false);
            }
            else
            {
                waterBarImages[i].gameObject.SetActive(true);
            }
        }
    }
}
