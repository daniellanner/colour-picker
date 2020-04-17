using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColourChannelOnDragBackground : MonoBehaviour, IColourChannelOnDragVisual
{
	private Image _background;

	private void Awake()
	{
		_background = GetComponent<Image>();
		ColourChooser.AddVisual(this);
	}

	public void UpdateColour(float h, float s, float v)
	{
		_background.color = Color.HSVToRGB(h, s, v);
	}
}
