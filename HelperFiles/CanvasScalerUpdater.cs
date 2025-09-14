using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerUpdater : MonoBehaviour
{
    public CanvasScaler scaler;

    protected void Awake()
    {
        if (scaler == null) scaler = GetComponent<CanvasScaler>();
    }

    protected void LateUpdate()
    {
        PixelPerfect.UpdateCanvasScaler(scaler);
    }
}