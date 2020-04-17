using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParallelAnimation
{

	public float Duration { get; set; }
	public float Delay { get; set; }
	public float T { get; set; }

	protected IInterpolationMethod _interpolation;
	protected System.Action _callback;

	public ParallelAnimation(float duration, IInterpolationMethod interpolation, System.Action callback = null, float delay = 0f)
	{
		Duration = duration;
		_interpolation = interpolation;
		_callback = callback;
		Delay = delay;
	}

	public abstract IEnumerator StartAnimation();

	public void ResetCallback()
	{
		_callback = null;
	}

	public void SetCallback(System.Action p_callback)
	{
		_callback = p_callback;
	}

	public void SetInterpolation(IInterpolationMethod p_interpolation)
	{
		_interpolation = p_interpolation;
	}
}

//Method Chaining
static class ParallelAnimationExtensionMethods
{
	static public T ResetDelta<T>(this T o) where T : ParallelAnimation
	{
		o.T = 0f;
		return o;
	}

	static public T Reset<T>(this T o) where T : ParallelAnimation
	{
		o.T = 0f;
		o.Delay = 0f;
		o.ResetCallback();
		return o;
	}

	static public T Delay<T>(this T o, float p_delay) where T : ParallelAnimation
	{
		o.Delay = p_delay;
		return o;
	}

	static public T Callback<T>(this T o, System.Action p_callback) where T : ParallelAnimation
	{
		o.SetCallback(p_callback);
		return o;
	}

	static public T Interpolation<T>(this T o, IInterpolationMethod p_interpolation) where T : ParallelAnimation
	{
		o.SetInterpolation(p_interpolation);
		return o;
	}
}
