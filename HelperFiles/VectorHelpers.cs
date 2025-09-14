using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class VectorHelpers
{
    public static Vector2 SnapToGrid(Vector2 vector, int cellsPerUnit)
    {
        return new Vector2(
            (float)Mathf.RoundToInt(vector.x * cellsPerUnit) / cellsPerUnit,
            (float)Mathf.RoundToInt(vector.y * cellsPerUnit) / cellsPerUnit);
    }

    public static Vector2 SnapToGrid(Vector2 vector, int cellsPerUnit, float z)
    {
        return new Vector3(
            (float)Mathf.RoundToInt(vector.x * cellsPerUnit) / cellsPerUnit,
            (float)Mathf.RoundToInt(vector.y * cellsPerUnit) / cellsPerUnit,
            z);
    }
}