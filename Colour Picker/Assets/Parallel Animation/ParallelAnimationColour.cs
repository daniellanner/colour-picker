using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallelAnimationColour : ParallelAnimation
{
    protected UnityEngine.UI.Graphic _graphic;
    protected Color _from;
    protected Color _to;

    public ParallelAnimationColour(UnityEngine.UI.Graphic graphic, Color from, Color to, float duration, IInterpolationMethod interpolation, System.Action callback = null, float delay = 0f) :
        base(duration, interpolation, callback, delay)
    {
        _graphic = graphic;
        _from = from;
        _to = to;
    }

    public void SetFrom(Color p_from)
    {
        _from = p_from;
    }

    public void SetTo(Color p_to)
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
                _graphic.color = Color.Lerp(_from, _to, _interpolation.Interpolate(T));
                T += Time.deltaTime / Duration;
                yield return null;
            }
        }

        T = 1f;
        _graphic.color = _to;
        _callback?.Invoke();
    }
}

static class ParallelAnimationColourExtensionMethods
{
    static public T From<T>(this T o, Color p_from) where T : ParallelAnimationColour
    {
        o.SetFrom(p_from);
        return o;
    }

    static public T To<T>(this T o, Color p_to) where T : ParallelAnimationColour
    {
        o.SetFrom(p_to);
        return o;
    }
}