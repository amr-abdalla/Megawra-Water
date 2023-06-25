using System.Collections;
using System;
using UnityEngine;

public class ErabyCollisionEvents : MonoBehaviour
{
    public Action<float, float> OnBump;
    public Action OnTrample;

     private void OnCollisionEnter2D(Collision2D other) {
        

        Debug.Log("Collision with " + other.gameObject.tag);

        if (other.gameObject.CompareTag("Trample"))
        {
            OnTrample?.Invoke();
        }
        else if (other.gameObject.CompareTag("Bump"))
        {
            float duration = other.gameObject
                .GetComponent<PlatformHorizontalCollider>()
                .getTimeDisabled();

            float magnitude = other.gameObject
                .GetComponent<PlatformHorizontalCollider>()
                .getBumpMagnitude();
            
        

            OnBump?.Invoke(magnitude, duration);

        }   
    }
}
