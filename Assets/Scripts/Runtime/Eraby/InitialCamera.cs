using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class InitialCamera : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCamera;

    [SerializeField]
    private float waitTime = 3.0f;

    private IEnumerator CameraSequence()
    {
        virtualCamera.Priority = 100;
        yield return new WaitForSeconds(waitTime);
        virtualCamera.Priority = 0;
    }

    private void StartCameraSequence(int _i_level)
    {
        StartCoroutine(CameraSequence());
    }

    private void Awake()
    {
        levelManager.RegisterToLevelStart(StartCameraSequence);
    }
}
