using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShabbaMoveAction
{
    void Push(float force, Vector2 direction);
    void Rotate(Vector2 direction);
}
