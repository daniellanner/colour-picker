using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Image))]
public class ColourChooser : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private IColourChannelImplementation _colourChannel;

    private static List<IColourChannelOnDragVisual> _visuals = new List<IColourChannelOnDragVisual>();

    private float _lastXPosition = 0f;
    private float _h, _s, _v;

    public Vector3 Colour => new Vector3(_h, _s, _v);

    private void Start()
    {
        _colourChannel = new ColourChannelH();
        Color.RGBToHSV(Color.red, out _h, out _s, out _v);

        foreach(var it in _visuals)
        {
            it.UpdateColour(_h, _s, _v);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastXPosition = eventData.position.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _colourChannel.UpdateColour(ref _h, ref _s, ref _v, (eventData.position.x - _lastXPosition) / Screen.width);
        _lastXPosition = eventData.position.x;


        foreach (var it in _visuals)
        {
            it.UpdateColour(_h, _s, _v);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
    }

    public void SetColourImplementation(IColourChannelImplementation p_newImplementation)
    {
        _colourChannel = p_newImplementation;
    }

    public static void AddVisual(IColourChannelOnDragVisual p_visual)
    {
        _visuals.Add(p_visual);
    }

}
