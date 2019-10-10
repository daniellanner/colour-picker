using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParallelAnimation3D : ParallelAnimation
{
    protected Transform _transform;
    protected Vector3 _from;
    protected Vector3 _to;

    public ParallelAnimation3D(Transform transform, Vector3 from, Vector3 to, float duration, IInterpolationMethod interpolation, System.Action callback = null, float delay = 0f) :
        base(duration, interpolation, callback, delay)
    {
        _transform = transform;
        _from = from;
        _to = to;
    }

    public void SetFrom(Vector3 p_from)
    {
        _from = p_from;
    }

    public void SetTo(Vector3 p_to)
    {
        _to = p_to;
    }
}

static class ParallelAnimation3DExtensionMethods
{
    static public T From<T>(this T o, Vector3 p_from) where T : ParallelAnimation3D
    {
        o.SetFrom(p_from);
        return o;
    }

    static public T To<T>(this T o, Vector3 p_to) where T : ParallelAnimation3D
    {
        o.SetTo(p_to);
        return o;
    }
}
