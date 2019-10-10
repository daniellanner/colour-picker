using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(Image))]
public class ChangeActiveColourChannelButtonUnderline : MonoBehaviour, IChangeActiveColourChannelButtonVisual
{
    [SerializeField]
    private List<LineWidth> lineWidth;

    private Dictionary<ChangeActiveColourChannelButton.TypeOfColourChannel, float> _buttonXPositions = new Dictionary<ChangeActiveColourChannelButton.TypeOfColourChannel, float>();
    private RectTransform _transform;
    private Image _line;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _line = GetComponent<Image>();

        ChangeActiveColourChannelButton.AddVisual(this);

        var allButtons = FindObjectsOfType<ChangeActiveColourChannelButton>();

        foreach (var it in allButtons)
        {
            _buttonXPositions.Add(it.Channel, it.gameObject.GetComponent<RectTransform>().anchoredPosition.x);
        }
    }

    public void Selected(ChangeActiveColourChannelButton.TypeOfColourChannel p_selectedChannel)
    {
        var position = _transform.anchoredPosition;

        if (_buttonXPositions.TryGetValue(p_selectedChannel, out float x))
        {
            position.x = x;
        }

        Vector2 newWidth = _transform.sizeDelta;
        newWidth.x = lineWidth.First(it => it.button == p_selectedChannel).width;

        StopAllCoroutines();

        StartCoroutine(new ParallelTranslation(_transform, _transform.anchoredPosition, position, .25f, new EaseInterpolation(3f)).StartAnimation());
        StartCoroutine(new ParallelSizeDelta(_transform, _transform.sizeDelta, newWidth, .125f, new EaseInterpolation(3f)).StartAnimation());

    }

    [System.Serializable]
    struct LineWidth
    {
        public ChangeActiveColourChannelButton.TypeOfColourChannel button;
        public float width;
    }
}
