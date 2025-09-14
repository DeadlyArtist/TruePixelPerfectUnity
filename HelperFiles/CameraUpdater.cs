using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraUpdater : MonoBehaviour
{
    public Camera camera;

    protected void Awake()
    {
        if (camera == null) camera = GetComponent<Camera>();

        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    }

    protected void OnDestroy()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    }

    protected void LateUpdate()
    {
        UpdateCameraPixelPerfect();
    }

    public void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        if (camera != this.camera) return;

        UpdateCameraPixelPerfect();
    }

    public void UpdateCameraPixelPerfect()
    {
        PixelPerfect.UpdateCameraPosition(camera);
        PixelPerfect.UpdateCameraSize(camera);
    }
}