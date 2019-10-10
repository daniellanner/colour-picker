using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(MouseClickVisual))]
public class MouseClickVisualRingImplementation : MonoBehaviour, IMouseClickVisualImplementation
{
    private float _initRadius = .5f;
    private float _initThickness = 0f;

    [SerializeField]
    private float _tapRadius = .35f;
    [SerializeField]
    private float _tapThickness = .25f;

    [SerializeField]
    private float _holdThickness = .3f;

    [SerializeField]
    private float _tapDuration = .25f;

    private RectTransform _transform;
    private Material _material;

    private int _radiusID;
    private int _thicknessID;

    private ParallelShaderFloat _radius;
    private ParallelShaderFloat _thickness;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        _material = GetComponent<Image>().material;
        GetComponent<MouseClickVisual>().SetVisualImplementation(this);

        _radiusID = Shader.PropertyToID("_Radius");
        _thicknessID = Shader.PropertyToID("_Thickness");

        _radius = new ParallelShaderFloat(
            _material,
            _radiusID,
            _initRadius,
            _tapRadius,
            _tapDuration,
            new EaseInterpolation());

        _thickness = new ParallelShaderFloat(
            _material,
            _thicknessID,
            _initThickness,
            _tapThickness,
            _tapDuration,
            new EaseInterpolation());
    }

    public void MouseDown(Vector2 p_position)
    {
        _transform.anchoredPosition = p_position;

        StopAllCoroutines();

        _material.SetFloat(_radiusID, _tapRadius);
        _thickness.Reset().From(_initThickness).To(.3f).Interpolation(new EaseInterpolation(3));
        _thickness.Duration = .125f;

        StartCoroutine(_thickness.StartAnimation());
    }

    public void Mouse(Vector2 p_position, float deltaT)
    {
        _transform.anchoredPosition = p_position;
    }

    public void MouseUp(Vector2 p_position)
    {
        StopAllCoroutines();

        _radius.Reset().From(_tapRadius).To(_initRadius).Interpolation(new EaseInterpolation(4));
        _thickness.Reset().From(_initThickness).To(_tapThickness).Interpolation(new SoftBell());
        _thickness.Duration = _tapDuration;

        StartCoroutine(_radius.StartAnimation());
        StartCoroutine(_thickness.StartAnimation());
    }
}
