using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DirtyMenu : MonoBehaviour
{
    // Start is called before the first frame update

    private ErabyInputActions inputMap;

    private void GoToEraby(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("ErabyPlayground");
    }

    private void GoToShabba(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("ShabbaLevel1");
    }

    void Awake()
    {
        inputMap = new ErabyInputActions();
        inputMap.Menu.Eraby.Enable();
        inputMap.Menu.Shabba.Enable();
        inputMap.Menu.Eraby.started += GoToEraby;
        inputMap.Menu.Shabba.started += GoToShabba;
    }

    // Update is called once per frame
}
