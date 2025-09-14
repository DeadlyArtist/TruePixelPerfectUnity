using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class SnapToGrid : MonoBehaviour
{
    public GameObject snapTarget;
    public GameObject realLocation; // uses snapTarget parent if empty

    protected void Awake()
    {
        if (snapTarget == null) snapTarget = gameObject;
        if (realLocation == null) realLocation = snapTarget.transform.parent.gameObject;
    }

    protected void LateUpdate()
    {
        PixelPerfect.SnapToGrid(snapTarget, realLocation);
    }
}