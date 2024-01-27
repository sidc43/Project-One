using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static Vector3[] GetWorldBounds(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);

        return v;
    }

    public static bool LMB() => Input.GetMouseButtonDown(0);
    public static bool RMB() => Input.GetMouseButtonDown(1);
}
