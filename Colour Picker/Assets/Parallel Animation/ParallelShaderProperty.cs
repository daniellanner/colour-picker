using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParallelShaderProperty : ParallelAnimation
{
    protected Material _material;
    protected int _propertyId;

    public ParallelShaderProperty(Material p_material, int p_propertyId, float duration, IInterpolationMethod interpolation, System.Action callback = null, float delay = 0f) :
    base(duration, interpolation, callback, delay)
    {
        _material = p_material;
        _propertyId = p_propertyId;
    }

    public ParallelShaderProperty(Material p_material, string p_property, float duration, IInterpolationMethod interpolation, System.Action callback = null, float delay = 0f) :
    base(duration, interpolation, callback, delay)
    {
        _material = p_material;
        _propertyId = Shader.PropertyToID(p_property);
    }

    public void SetMaterial(Material p_material)
    {
        _material = p_material;
    }

    public void SetProperty(int p_property)
    {
        _propertyId = p_property;
    }
}

static class ParallelShaderPropertyExtensionMethods
{
    static public T Material<T>(this T o, Material p_material) where T : ParallelShaderProperty
    {
        o.SetMaterial(p_material);
        return o;
    }

    static public T ShaderProperty<T>(this T o, int p_property) where T : ParallelShaderProperty
    {
        o.SetProperty(p_property);
        return o;
    }

    static public T ShaderProperty<T>(this T o, string p_property) where T : ParallelShaderProperty
    {
        o.SetProperty(Shader.PropertyToID(p_property));
        return o;
    }
}