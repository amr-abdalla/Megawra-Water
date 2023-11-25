using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Transform body = null;

    [SerializeField]
    float yCutoff = 0;

    [SerializeField]

    CinemachineVirtualCamera  groundCamera = null;
    const int lowPrio = 5;
    const int highPrio = 15;

    // Update is called once per frame
    void Update()
    {
        // if the body is below the yCutoff, switch to the player camera
        if (body.position.y > yCutoff)
        {
            groundCamera.Priority = highPrio;
        }
        else
        {
            groundCamera.Priority = lowPrio;
        }
    }
}
