using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInterpolationMethod
{
	float Interpolate(float x);
}

public class EaseInterpolation : IInterpolationMethod
{
	private float _easeFactor = 2f;

	public EaseInterpolation(float p_easeFactor = 2f)
	{
		p_easeFactor = _easeFactor;
	}

	public float Interpolate(float x)
	{
		return Mathf.Pow(x, _easeFactor) / (Mathf.Pow(x, _easeFactor) + Mathf.Pow(1f - x, _easeFactor));
	}
}

public class ExponentialInterpolation : IInterpolationMethod
{
	private float _easeFactor = 2f;

	public ExponentialInterpolation(float p_easeFactor = 2f)
	{
		p_easeFactor = _easeFactor;
	}

	public float Interpolate(float x)
	{
		return Mathf.Pow(x, _easeFactor);
	}
}

public class InverseExponentialInterpolation : IInterpolationMethod
{
	private float _easeFactor = 2f;

	public InverseExponentialInterpolation(float p_easeFactor = 2f)
	{
		p_easeFactor = _easeFactor;
	}

	public float Interpolate(float x)
	{
		return 1f - Mathf.Pow(1f - x, _easeFactor);
	}
}

public class LinearInterpolation : IInterpolationMethod
{
	public float Interpolate(float x)
	{
		return x;
	}
}

public class LinearBell : IInterpolationMethod
{
	public float Interpolate(float x)
	{
		return Mathf.Sin(x * Mathf.PI * 2);
	}
}

public class SoftBell : IInterpolationMethod
{
	public float Interpolate(float x)
	{
		return (Mathf.Sin(x * Mathf.PI * 2 - (Mathf.PI * .5f)) + 1f) * .5f;
	}
}