using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ChangeActiveColourChannelButtonFont : MonoBehaviour, IChangeActiveColourChannelButtonVisual
{
    [SerializeField]
    private Font _idle;
    [SerializeField]
    private Font _active;

    private Text _text;
    
    private bool _currentlyActive;
    private ChangeActiveColourChannelButton.TypeOfColourChannel _channel;
    private ColourChooser _chooser;

    private void Awake()
    {
        ChangeActiveColourChannelButton.AddVisual(this);
 
        var button = GetComponentInParent<ChangeActiveColourChannelButton>();

        _chooser = FindObjectOfType<ColourChooser>();

        if(button != null)
        {
            _channel = button.Channel;
        }
        else
        {
            Debug.LogError("No Button Found");
        }


        _text = GetComponent<Text>();
    }

    public void Selected(ChangeActiveColourChannelButton.TypeOfColourChannel p_selectedChannel)
    {
        bool newActive = p_selectedChannel.Equals(_channel);

        if(_currentlyActive == newActive)
        {
            return;
        }

        _currentlyActive = newActive;
        _text.font = _currentlyActive ? _active : _idle;
    }
}
