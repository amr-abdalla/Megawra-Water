using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTracker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform followedObject = null;

    void Update()
    {
        if (followedObject == null)
            return;
        // follow the x coordinates of the followed object
        transform.position = new Vector3(
            followedObject.position.x,
            transform.position.y,
            transform.position.z
        );
    }
}
