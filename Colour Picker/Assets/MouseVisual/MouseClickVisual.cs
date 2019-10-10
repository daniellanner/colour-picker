using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseClickVisual : MonoBehaviour
{
    private IMouseClickVisualImplementation _visualImplementation;
    private bool _implInit;

    public void SetVisualImplementation(IMouseClickVisualImplementation p_implementation)
    {
        _visualImplementation = p_implementation;
        _implInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_implInit)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _visualImplementation.MouseDown(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            _visualImplementation.Mouse(Input.mousePosition, Time.deltaTime);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _visualImplementation.MouseUp(Input.mousePosition);
        }
    }
}
