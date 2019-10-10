using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelShaderFloat : ParallelShaderProperty
{
    private float _from;
    private float _to;

    public ParallelShaderFloat(Material p_material, int p_propertyId, float from, float to, float duration, IInterpolationMethod interpolation, System.Action callback = null, float delay = 0f) :
        base(p_material, p_propertyId, duration, interpolation, callback, delay)
    {
        _from = from;
        _to = to;
    }

    public ParallelShaderFloat(Material p_material, string p_property, float from, float to, float duration, IInterpolationMethod interpolation, System.Action callback = null, float delay = 0f) :
    base(p_material, p_property, duration, interpolation, callback, delay)
    {
        _from = from;
        _to = to;
    }

    public void SetFrom(float p_from)
    {
        _from = p_from;
    }

    public void SetTo(float p_to)
    {
        _to = p_to;
    }

    public override IEnumerator StartAnimation()
    {
        yield return new WaitForSecondsRealtime(Delay);

        if (Duration > 0f)
        {
            T = 0;
            while (T <= 1f)
            {
                _material.SetFloat(_propertyId, Mathf.Lerp(_from, _to, _interpolation.Interpolate(T)));
                T += Time.deltaTime / Duration;
                yield return null;
            }
        }

        T = 1f;
        _material.SetFloat(_propertyId, Mathf.Lerp(_from, _to, _interpolation.Interpolate(T)));
        _callback?.Invoke();
    }
}

static class ParallelShaderFloatExtensionMethods
{
    static public T From<T>(this T o, float from) where T : ParallelShaderFloat
    {
        o.SetFrom(from);
        return o;
    }

    static public T To<T>(this T o, float to) where T : ParallelShaderFloat
    {
        o.SetTo(to);
        return o;
    }

}