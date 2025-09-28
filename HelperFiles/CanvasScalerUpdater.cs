using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(40)]
public class CanvasUpdater : MonoBehaviour
{
    public Camera camera;
    public Canvas canvas;
    public CanvasScaler scaler;

    protected void Awake()
    {
        if (canvas == null) canvas = GetComponent<Canvas>();
        if (scaler == null) scaler = GetComponent<CanvasScaler>();
        if (camera == null) camera = Camera.main;

        canvas.worldCamera = camera;
        canvas.renderMode = RenderMode.WorldSpace;
    }

    protected void LateUpdate()
    {
        PixelPerfect.UpdateCanvas(canvas);
        PixelPerfect.UpdateCanvasScaler(scaler);
    }
}