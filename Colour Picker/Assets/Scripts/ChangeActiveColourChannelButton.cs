using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class ChangeActiveColourChannelButton : MonoBehaviour
{
    public enum TypeOfColourChannel { Hue, Saturation, Value};

    [SerializeField]
    private TypeOfColourChannel _channel;
    public TypeOfColourChannel Channel => _channel;

    private static List<IChangeActiveColourChannelButtonVisual> _visuals = new List<IChangeActiveColourChannelButtonVisual>();

    public static void AddVisual(IChangeActiveColourChannelButtonVisual p_visual)
    {
        _visuals.Add(p_visual);
    }

    private void Awake()
    {
        ColourChooser chooser = FindObjectOfType<ColourChooser>();

        if (chooser != null)
        {
            GetComponent<Button>().onClick.AddListener(() => {

                IColourChannelImplementation colourChannel;

                switch (_channel)
                {
                    case TypeOfColourChannel.Hue:
                        colourChannel = new ColourChannelH();
                        break;
                    case TypeOfColourChannel.Saturation:
                        colourChannel = new ColourChannelS();
                        break;
                    case TypeOfColourChannel.Value:
                        colourChannel = new ColourChannelV();
                        break;
                    default:
                        colourChannel = new ColourChannelH();
                        break;
                }

                chooser.SetColourImplementation(colourChannel);
                
                foreach(var i in _visuals)
                {
                    i.Selected(_channel);
                }
            });
        }
    }

    private void Start()
    {
        if(_channel == TypeOfColourChannel.Hue)
        {
            foreach (var i in _visuals)
            {
                i.Selected(_channel);
            }
        }
    }
}
