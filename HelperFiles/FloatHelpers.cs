using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class FloatHelpers
{
    public static float Round(this float number, float stepSize)
    {
        return Mathf.Round(number / stepSize) * stepSize;
    }

    public static float Ceil(this float number, float stepSize)
    {
        return Mathf.Ceil(number / stepSize) * stepSize;
    }

    public static float Floor(this float number, float stepSize)
    {
        return Mathf.Floor(number / stepSize) * stepSize;
    }
}