using System.Collections;
using System;
using UnityEngine;

// OnCollisonEnter2D doesn't seem to work when a gameobject has a PhysicsBody2D component.
// This component is intended to be attached to a child gameobject (with a rigidbody2D component) to handle the non-grounded collisions.
public class ErabyRBCollisionHandler : MonoBehaviour
{
    public Action<float, float, Vector2> OnBump;
    public Action OnTrample;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        // check if collision is with a wall
        // if so, invoke OnBump with the direction of the wall
        // if not do nothing

        IWallPlatform wallPlatform = other.gameObject.GetComponent<IWallPlatform>();
        if (null == wallPlatform)
            return;

        Vector2 direction = other.GetContact(0).normal.x * Vector2.right;

        OnBump?.Invoke(wallPlatform.BumpMagnitude, wallPlatform.OnBumpTimeDisabled, direction);
        Debug.Log("Invoked Bump");
    }
}
