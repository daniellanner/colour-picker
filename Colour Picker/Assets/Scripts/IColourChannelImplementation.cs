using UnityEngine;

public interface IColourChannelImplementation
{
    void UpdateColour(ref float p_h, ref float p_s, ref float p_v, float p_screenWidthMovedDelta);
}

public class ColourChannelH : IColourChannelImplementation
{
    public void UpdateColour(ref float p_h, ref float p_s, ref float p_v, float p_screenWidthMovedDelta)
    {
        p_h += p_screenWidthMovedDelta * .5f;

        if(p_h < 0f)
        {
            p_h = 1f;
        }
        else if(p_h > 1f)
        {
            p_h = 0f;
        }
    }
}

public class ColourChannelS : IColourChannelImplementation
{
    public void UpdateColour(ref float p_h, ref float p_s, ref float p_v, float p_screenWidthMovedDelta)
    {
        p_s += p_screenWidthMovedDelta;
        p_s = Mathf.Clamp01(p_s);
    }
}

public class ColourChannelV : IColourChannelImplementation
{
    public void UpdateColour(ref float p_h, ref float p_s, ref float p_v, float p_screenWidthMovedDelta)
    {
        p_v += p_screenWidthMovedDelta;
        p_v = Mathf.Clamp01(p_v);
    }
}