using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;

    [SerializeField]
    [Range(0, 1)]
    private float parallaxFactor = 0.5f;

    private float prevCameraX;

    private void Start()
    {
        prevCameraX = mainCamera.transform.position.x;
    }

    void Update()
    {
        float deltaX = mainCamera.transform.position.x - prevCameraX;
        transform.position += Vector3.right * (deltaX * parallaxFactor);
        prevCameraX = mainCamera.transform.position.x;
    }
}
