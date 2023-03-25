using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShabbaMoveAction
{
    void Push(float force);
    void Rotate(Vector2 direction);
}
