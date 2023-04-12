using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "ShabbaAngularAccelerationData", menuName = "ScriptableObjects/ShabbaAngularAcceleration", order = 1)]
public class AngularAccelerationData : ScriptableObject
{
    public float maxAngularVelocity;
    public float angularAcceleration;
    public float angularDesceleration;
}
