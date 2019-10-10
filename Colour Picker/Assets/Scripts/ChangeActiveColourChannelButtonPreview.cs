using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChangeActiveColourChannelButtonPreview : MonoBehaviour, IChangeActiveColourChannelButtonVisual, IColourChannelOnDragVisual
{
    [SerializeField]
    [Range(0f,.5f)]
    private float _huePreviewDistance = .1f;

    [SerializeField]
    [Range(0f, .5f)]
    private float _saturationPreviewDistance = .25f;

    [SerializeField]
    [Range(0f, .5f)]
    private float _valuePreviewDistance = .25f;

    private ColourChooser _color;
    private ChangeActiveColourChannelButton.TypeOfColourChannel _activeChannel = ChangeActiveColourChannelButton.TypeOfColourChannel.Hue;

    private int _leftColorID;
    private int _centerColorID;
    private int _rightColorID;

    private Material _previewMaterial;

    private void Awake()
    {
        ColourChooser.AddVisual(this);
        ChangeActiveColourChannelButton.AddVisual(this);

        _color = FindObjectOfType<ColourChooser>();

        _leftColorID = Shader.PropertyToID("_LeftColor");
        _centerColorID = Shader.PropertyToID("_CenterColor");
        _rightColorID = Shader.PropertyToID("_RightColor");

        _previewMaterial = GetComponent<Image>().material;
    }

    public void Selected(ChangeActiveColourChannelButton.TypeOfColourChannel p_selectedChannel)
    {
        if(p_selectedChannel == _activeChannel)
        {
            return;
        }

        _activeChannel = p_selectedChannel;

        UpdateColour(_color.Colour.x, _color.Colour.y, _color.Colour.z);
    }

    private Vector3 GetLeftColour(Vector3 p_baseColour, ChangeActiveColourChannelButton.TypeOfColourChannel p_selectedChannel)
    {
        switch (p_selectedChannel)
        {
            case ChangeActiveColourChannelButton.TypeOfColourChannel.Hue:

                float hue = p_baseColour.x;
                hue -= _huePreviewDistance;

                if(hue < 0f)
                {
                    hue = 1 + hue;
                }

                p_baseColour.x = Mathf.Clamp01(hue);
                break;

            case ChangeActiveColourChannelButton.TypeOfColourChannel.Saturation:

                float saturation = p_baseColour.y;
                saturation -= _saturationPreviewDistance;
                p_baseColour.y = Mathf.Clamp01(saturation);
                break;

            case ChangeActiveColourChannelButton.TypeOfColourChannel.Value:

                float value = p_baseColour.z;
                value -= _valuePreviewDistance;
                p_baseColour.z = Mathf.Clamp01(value);
                break;
        }

        return p_baseColour;
    }

    private Vector3 GetRightColour(Vector3 p_baseColour, ChangeActiveColourChannelButton.TypeOfColourChannel p_selectedChannel)
    {
        switch (p_selectedChannel)
        {
            case ChangeActiveColourChannelButton.TypeOfColourChannel.Hue:

                float hue = p_baseColour.x;
                hue += _huePreviewDistance;

                if (hue > 1f)
                {
                    hue = hue - 1f;
                }

                p_baseColour.x = Mathf.Clamp01(hue);
                break;

            case ChangeActiveColourChannelButton.TypeOfColourChannel.Saturation:

                float saturation = p_baseColour.y;
                saturation += _saturationPreviewDistance;
                p_baseColour.y = Mathf.Clamp01(saturation);
                break;

            case ChangeActiveColourChannelButton.TypeOfColourChannel.Value:

                float value = p_baseColour.z;
                value += _valuePreviewDistance;
                p_baseColour.z = Mathf.Clamp01(value);
                break;
        }

        return p_baseColour;
    }

    public void UpdateColour(float h, float s, float v)
    {
        Vector3 baseColor = new Vector3(h,s,v);
        Vector3 tmpColor = baseColor;

        //tmpColor.z = tmpColor.z / 4f + .75f;
        Color centerColor = Color.HSVToRGB(tmpColor.x, tmpColor.y, tmpColor.z);

        tmpColor = GetLeftColour(baseColor, _activeChannel);
        //tmpColor.z = tmpColor.z / 4f + .75f;
        Color leftColor = Color.HSVToRGB(tmpColor.x, tmpColor.y, tmpColor.z);

        tmpColor = GetRightColour(baseColor, _activeChannel);
        //tmpColor.z = tmpColor.z / 4f + .75f;
        Color rightColor = Color.HSVToRGB(tmpColor.x, tmpColor.y, tmpColor.z);

        _previewMaterial.SetColor(_leftColorID, leftColor);
        _previewMaterial.SetColor(_centerColorID, centerColor);
        _previewMaterial.SetColor(_rightColorID, rightColor);
    }
}
