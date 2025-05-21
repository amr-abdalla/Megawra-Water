using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SceneTransitionHelper))]
public class ExitToEntryOnQuitButton : MonoBehaviour
{
    private SceneTransitionHelper sceneTransitionHelper;
    [SerializeField] private InputActionReference exitToMenuAction;
    private void Awake()
    {
    }
    private void Start()
    {
        exitToMenuAction.action.Enable();
        sceneTransitionHelper = GetComponent<SceneTransitionHelper>();
    }

    private void OnEnable()
    {
        exitToMenuAction.action.performed += HandleExitToMenuActionHandler;
    }

    private void OnDisable()
    {
        exitToMenuAction.action.performed -= HandleExitToMenuActionHandler;
    }

    private void HandleExitToMenuActionHandler(InputAction.CallbackContext obj)
    {
        sceneTransitionHelper.LoadScene("Entry Scene");
    }
}
