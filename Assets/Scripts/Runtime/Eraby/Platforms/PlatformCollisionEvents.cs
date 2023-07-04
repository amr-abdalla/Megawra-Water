using UnityEngine;
using System.Collections;
using System;

public class PlatformCollisionEvents : MonoBehaviour
{
    public Action<Vector2> OnBump;
    public Action OnBounce;
}
