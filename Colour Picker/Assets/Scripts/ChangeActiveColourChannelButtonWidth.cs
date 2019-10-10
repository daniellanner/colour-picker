using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ChangeActiveColourChannelButtonWidth : MonoBehaviour, IChangeActiveColourChannelButtonVisual
{    
    private bool _currentlyActive;
    private ChangeActiveColourChannelButton.TypeOfColourChannel _channel;
    private RectTransform _transform;

    private void Awake()
    {
        ChangeActiveColourChannelButton.AddVisual(this);

        _transform = GetComponent<RectTransform>();

        var button = GetComponent<ChangeActiveColourChannelButton>();
        if(button != null)
        {
            _channel = button.Channel;
        }
        else
        {
            Debug.LogError("No Button Found");
        }
    }

    public void Selected(ChangeActiveColourChannelButton.TypeOfColourChannel p_selectedChannel)
    {
        bool newActive = p_selectedChannel.Equals(_channel);

        if(_currentlyActive == newActive)
        {
            return;
        }

        _currentlyActive = newActive;

        var sizeDelta = _transform.sizeDelta;
        sizeDelta.x = _currentlyActive ? 128 + 32 : 128;
        _transform.sizeDelta = sizeDelta;
    }
}
