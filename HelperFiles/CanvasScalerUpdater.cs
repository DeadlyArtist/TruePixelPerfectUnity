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
        var rectTransform = canvas.transform as RectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;

        canvas.worldCamera = camera;
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.pixelPerfect = true;
        scaler.matchWidthOrHeight = 1f;
    }

    protected void LateUpdate()
    {
        PixelPerfect.UpdateCanvas(canvas);
        PixelPerfect.UpdateCanvasScaler(scaler);
    }
}