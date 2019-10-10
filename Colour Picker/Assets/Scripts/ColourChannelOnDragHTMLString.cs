using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ColourChannelOnDragHTMLString : MonoBehaviour, IColourChannelOnDragVisual
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
        ColourChooser.AddVisual(this);
    }

    public void UpdateColour(float h, float s, float v)
    {
        _text.text = "#" + ColorUtility.ToHtmlStringRGB(Color.HSVToRGB(h, s, v));
    }
}
