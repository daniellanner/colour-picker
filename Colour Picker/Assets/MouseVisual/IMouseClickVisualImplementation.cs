using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMouseClickVisualImplementation
{
    void MouseDown(Vector2 p_position);
    void Mouse(Vector2 p_position, float deltaT);
    void MouseUp(Vector2 p_position);
}
