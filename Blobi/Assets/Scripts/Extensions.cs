using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Extensions
{
    public static System.Numerics.Vector2 To(this UnityEngine.Vector2 v)
    {
        return new System.Numerics.Vector2(v.x, v.y);
    }

    public static UnityEngine.Vector2 To(this System.Numerics.Vector2 v)
    {
        return new UnityEngine.Vector2(v.X, v.Y);
    }
}