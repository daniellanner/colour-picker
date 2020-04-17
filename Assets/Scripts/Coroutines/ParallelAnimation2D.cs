using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParallelAnimation2D : ParallelAnimation
{
	protected RectTransform _transform;
	protected Vector2 _from;
	protected Vector2 _to;

	public ParallelAnimation2D(RectTransform transform, Vector2 from, Vector3 to, float duration, IInterpolationMethod interpolation, System.Action callback = null, float delay = 0f) :
			base(duration, interpolation, callback, delay)
	{
		_transform = transform;
		_from = from;
		_to = to;
	}

	public void SetFrom(Vector2 p_from)
	{
		_from = p_from;
	}

	public void SetTo(Vector2 p_to)
	{
		_to = p_to;
	}
}

static class ParallelAnimation2DExtensionMethods
{
	static public T From<T>(this T o, Vector2 p_from) where T : ParallelAnimation2D
	{
		o.SetFrom(p_from);
		return o;
	}

	static public T To<T>(this T o, Vector2 p_to) where T : ParallelAnimation2D
	{
		o.SetTo(p_to);
		return o;
	}
}
