using UnityEngine;

public static class MyExtension
{
    public static Color WithAlpha(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}