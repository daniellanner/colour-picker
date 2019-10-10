using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallelSizeDelta : ParallelAnimation2D{

    public ParallelSizeDelta(RectTransform transform, Vector2 from, Vector2 to, float duration, IInterpolationMethod interpolation, Action callback = null, float delay = 0f) :
        base(transform, from, to, duration, interpolation, callback, delay)
    {
    }

    public override IEnumerator StartAnimation()
    {
        yield return new WaitForSecondsRealtime(Delay);

        if (Duration > 0f)
        {
            T = 0;
            while (T <= 1f)
            {
                _transform.sizeDelta = Vector2.Lerp(_from, _to, _interpolation.Interpolate(T));
                T += Time.deltaTime / Duration;
                yield return null;
            }
        }

        T = 1f;
        _transform.sizeDelta = _to;
        _callback?.Invoke();
    }
}

