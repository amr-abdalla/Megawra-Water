using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;

    [SerializeField]
    [Range(0, 1)]
    private float parallaxFactor = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    private float parallaxYFactor = 0.5f;

    private float prevCameraX;
    private float prevCameraY;

    private void Start()
    {
        prevCameraX = mainCamera.transform.position.x;
        prevCameraY = mainCamera.transform.position.y;
    }

    void Update()
    {
        float deltaX = mainCamera.transform.position.x - prevCameraX;
        transform.position += Vector3.right * (deltaX * parallaxFactor);
        prevCameraX = mainCamera.transform.position.x;

        float deltaY = mainCamera.transform.position.y - prevCameraY;
        transform.position += Vector3.up * (deltaY * parallaxYFactor);
        prevCameraY = mainCamera.transform.position.y;

    }
}
