using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[DefaultExecutionOrder(-1100)]
[ExecuteInEditMode]
public class PixelPerfect : PersistentSingleton<PixelPerfect>
{
	public static bool Enabled { get => Instance.enabled && PerfectPixelsPerUnit != 0; set => Instance.enabled = value; }
	public static bool GUIEnabled { get => Instance.guiEnabled && PerfectPixelsPerUnit != 0; set => Instance.enabled = value; }
	public static Rect PixelRect { get; private set; }
	public static int SpritePixelsPerUnit { get => Instance.spritePixelsPerUnit; set => Instance.spritePixelsPerUnit = value; }
	public static float UnitsPerSpritePixel { get; private set; }
	public static int MaxVerticalSpritePixels { get => Instance.maxVerticalSpritePixels; set => Instance.maxVerticalSpritePixels = value; }
	public static int TargetGUISizeReduction { get => Instance.targetGUISizeReduction; set => Instance.targetGUISizeReduction = value; }
	public static float GuiScale { get => Instance.guiScale; set => Instance.guiScale = value; }

	// For pixel perfect
	public static int PerfectPixelsPerUnit { get; private set; }
	public static float PerfectUnitsPerPixel { get; private set; }
	public static int PerfectPixelsPerSpritePixel { get; private set; }
	public static int PerfectGUIUnitsPerSpritePixel { get; private set; }
	public static int PerfectGUIUnitsPerUnit { get; private set; }
	public static float PerfectGUIUnitsPerPixel { get; private set; }

	public static float UnevenPixelHeightOffset { get; private set; }
	public static float UnevenPixelWidthOffset { get; private set; }

	// For non pixel perfect
	public static float OrthographicSize { get; private set; }
	public static float ImperfectPixelsPerUnit { get; private set; }
	public static float ImperfectUnitsPerPixel { get; private set; }
	public static float ImperfectPixelsPerSpritePixel { get; private set; }

	public const float AspectRatio = 16 / 9f;
	public static Vector2 ReferenceResolution { get; private set; }
	public static float ImperfectGUIUnitsPerSpritePixel { get; private set; }
	public static float ImperfectGUIUnitsPerUnit { get; private set; }
	public static float ImperfectGUIUnitsPerPixel { get; private set; }

	// Unified
	public static float PixelsPerUnit { get; private set; }
	public static float UnitsPerPixel { get; private set; }
	public static float PixelsPerSpritePixel { get; private set; }
	public static float GUIUnitsPerSpritePixel { get; private set; }
	public static float GUIUnitsPerUnit { get; private set; }
	public static float GUIUnitsPerPixel { get; private set; }
	public static float UnitsPerGUIUnit { get; private set; }


	[SerializeField] private new bool enabled = true;
	[SerializeField] private bool guiEnabled = true;
	[SerializeField][Display(nameof(UnitsPerSpritePixel))] private float unitsPerSpritePixel;
	[SerializeField] private int spritePixelsPerUnit = 16;
	[SerializeField] private int maxVerticalSpritePixels = 180;
	[SerializeField] private int targetGUISizeReduction = 0;
	[SerializeField][Range(0.1f, 1f)] private float guiScale = 1f;

	//[SerializeField][Display(nameof(PerfectPixelsPerUnit))] private int perfectPixelsPerUnit;
	//[SerializeField][Display(nameof(PerfectUnitsPerPixel))] private float perfectUnitsPerPixel;
	//[SerializeField][Display(nameof(PerfectPixelsPerSpritePixel))] private int perfectPixelsPerSpritePixel;
	//[SerializeField][Display(nameof(PerfectGUIUnitsPerSpritePixel))] private int perfectGUIUnitsPerSpritePixel;
	//[SerializeField][Display(nameof(PerfectGUIUnitsPerUnit))] private int perfectGUIUnitsPerUnit;
	//[SerializeField][Display(nameof(PerfectGUIUnitsPerPixel))] private float perfectGUIUnitsPerPixel;

	[SerializeField][Display(nameof(UnevenPixelHeightOffset))] private float unevenPixelHeightOffset;
	[SerializeField][Display(nameof(UnevenPixelWidthOffset))] private float unevenPixelWidthOffset;

	[SerializeField][Display(nameof(OrthographicSize))] private float orthographicSize;
	//[SerializeField][Display(nameof(ImperfectPixelsPerUnit))] private float imperfectPixelsPerUnit;
	//[SerializeField][Display(nameof(ImperfectUnitsPerPixel))] private float imperfectUnitsPerPixel;
	//[SerializeField][Display(nameof(ImperfectPixelsPerSpritePixel))] private float imperfectPixelsPerSpritePixel;

	[SerializeField][Display(nameof(ReferenceResolution))] private Vector2 referenceResolution;
	//[SerializeField][Display(nameof(ImperfectGUIUnitsPerSpritePixel))] private float imperfectGUIUnitsPerSpritePixel;
	//[SerializeField][Display(nameof(ImperfectGUIUnitsPerUnit))] private float imperfectGUIUnitsPerUnit;
	//[SerializeField][Display(nameof(ImperfectGUIUnitsPerPixel))] private float imperfectGUIUnitsPerPixel;

	[SerializeField][Display(nameof(PixelsPerUnit))] private float pixelsPerUnit;
	[SerializeField][Display(nameof(UnitsPerPixel))] private float unitsPerPixel;
	[SerializeField][Display(nameof(PixelsPerSpritePixel))] private float pixelsPerSpritePixel;
	[SerializeField][Display(nameof(GUIUnitsPerSpritePixel))] private float guiUnitsPerSpritePixel;
	[SerializeField][Display(nameof(GUIUnitsPerUnit))] private float guiUnitsPerUnit;
	[SerializeField][Display(nameof(GUIUnitsPerPixel))] private float guiUnitsPerPixel;
	[SerializeField][Display(nameof(UnitsPerGUIUnit))] private float unitsPerGUIUnit;

	protected virtual void Start()
	{
		RenderPipelineManager.beginContextRendering += OnBeginContextRendering;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		RenderPipelineManager.beginContextRendering -= OnBeginContextRendering;
	}

	protected virtual void LateUpdate()
	{
		UpdatePixelData();
	}

	protected void OnBeginContextRendering(ScriptableRenderContext context, List<Camera> cameras)
	{
		UpdatePixelData();
	}

	public static void UpdatePixelData()
	{
		var camera = CameraManager.mainCamera;
		if (camera != null || (camera = Camera.main) != null)
		{
			PixelRect = camera.pixelRect;
		}
		else
		{
			PixelRect = new Rect(0, 0, Screen.width, Screen.height);
		}

		UnitsPerSpritePixel = 1f / SpritePixelsPerUnit;
		PerfectPixelsPerSpritePixel = Mathf.CeilToInt(PixelRect.height / MaxVerticalSpritePixels);
		PerfectPixelsPerUnit = PerfectPixelsPerSpritePixel * SpritePixelsPerUnit;
		PerfectUnitsPerPixel = 1f / PerfectPixelsPerUnit;
		UnevenPixelHeightOffset = (PixelRect.height % 2 != 0) ? PerfectUnitsPerPixel / 2f : 0f;
		UnevenPixelWidthOffset = (PixelRect.width % 2 != 0) ? PerfectUnitsPerPixel / 2f : 0f;

		PerfectGUIUnitsPerSpritePixel = Mathf.Max(1, PerfectPixelsPerSpritePixel - GameSettings.TargetGUISizeReduction);
		PerfectGUIUnitsPerUnit = PerfectGUIUnitsPerSpritePixel * SpritePixelsPerUnit;
		PerfectGUIUnitsPerPixel = 1f / PerfectGUIUnitsPerUnit;

		OrthographicSize = MaxVerticalSpritePixels * UnitsPerSpritePixel / 2;
		ImperfectPixelsPerSpritePixel = PixelRect.height / MaxVerticalSpritePixels;
		ImperfectPixelsPerUnit = ImperfectPixelsPerSpritePixel * SpritePixelsPerUnit;
		ImperfectUnitsPerPixel = 1f / ImperfectPixelsPerUnit;

		ReferenceResolution = new Vector2(MaxVerticalSpritePixels * AspectRatio, MaxVerticalSpritePixels);
		ImperfectGUIUnitsPerSpritePixel = ImperfectPixelsPerSpritePixel * GameSettings.GuiScale;
		ImperfectGUIUnitsPerUnit = ImperfectGUIUnitsPerSpritePixel * SpritePixelsPerUnit;
		ImperfectGUIUnitsPerPixel = 1f / ImperfectGUIUnitsPerUnit;

		if (Enabled)
		{
			PixelsPerSpritePixel = PerfectPixelsPerSpritePixel;
			PixelsPerUnit = PerfectPixelsPerUnit;
			UnitsPerPixel = PerfectUnitsPerPixel;
		}
		else
		{
			PixelsPerSpritePixel = ImperfectPixelsPerSpritePixel;
			PixelsPerUnit = ImperfectPixelsPerUnit;
			UnitsPerPixel = ImperfectUnitsPerPixel;
		}

		if (GUIEnabled)
		{
			GUIUnitsPerSpritePixel = PerfectGUIUnitsPerSpritePixel;
			GUIUnitsPerUnit = PerfectGUIUnitsPerUnit;
			GUIUnitsPerPixel = PerfectGUIUnitsPerPixel;
		}
		else
		{
			GUIUnitsPerSpritePixel = ImperfectGUIUnitsPerSpritePixel;
			GUIUnitsPerUnit = ImperfectGUIUnitsPerUnit;
			GUIUnitsPerPixel = ImperfectGUIUnitsPerPixel;
		}

		UnitsPerGUIUnit = UnitsPerSpritePixel * GUIUnitsPerSpritePixel / PixelsPerSpritePixel;
	}

	public static void UpdateCameraSize(Camera camera)
	{
		if (!Enabled)
		{
			ResetCameraSize(camera);
			return;
		}

		float orthoSize = PixelRect.height / PerfectPixelsPerUnit;
		camera.orthographicSize = orthoSize / 2;
	}

	public static void UpdateCameraPosition(Camera camera)
	{
		if (!Enabled)
		{
			ResetCameraPosition(camera);
			return;
		}

		Vector3 position = camera.transform.localPosition;
		position.x = UnevenPixelWidthOffset;
		position.y = UnevenPixelHeightOffset;
		camera.transform.localPosition = position.With(z: position.z);
	}

	public static void ResetCameraSize(Camera camera)
	{
		camera.orthographicSize = OrthographicSize;
	}

	public static void ResetCameraPosition(Camera camera)
	{
		camera.transform.localPosition = new Vector3(0, 0, camera.transform.localPosition.z);
	}

	public static void UpdateCanvasScaler(CanvasScaler scaler)
	{
		if (GUIEnabled)
		{
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
			scaler.referencePixelsPerUnit = PerfectPixelsPerUnit;
			scaler.scaleFactor = PerfectGUIUnitsPerSpritePixel;
		}
		else
		{
			scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			scaler.referenceResolution = ReferenceResolution / GameSettings.GuiScale;
		}
	}

	public static float RoundToPixels(float value)
	{
		if (!Enabled) return value;
		return value.Round(PerfectUnitsPerPixel);
	}

	public static float CeilToPixels(float value)
	{
		if (!Enabled) return value;
		return value.Ceil(PerfectUnitsPerPixel);
	}

	public static float FloorToPixels(float value)
	{
		if (!Enabled) return value;
		return value.Floor(PerfectUnitsPerPixel);
	}

	public static float RoundWorldToGUIUnits(float value)
	{
		if (!GUIEnabled) return value;
		return value.Round(UnitsPerGUIUnit);
	}

	public static Vector2 SnapToGrid(Vector2 target)
	{
		if (!Enabled) return target;
		return VectorHelpers.SnapToGrid(target, PerfectPixelsPerUnit);
	}

	public static Vector3 SnapToGrid(Vector2 target, float z)
	{
		if (!Enabled) return target.ToVector3(z);
		return VectorHelpers.SnapToGrid(target, PerfectPixelsPerUnit, z);
	}

	public static void SnapToGrid(GameObject gameObject, GameObject realLocation)
	{
		if (!Enabled) return;
		gameObject.transform.position = VectorHelpers.SnapToGrid(realLocation.transform.position, PerfectPixelsPerUnit, gameObject.transform.position.z);
	}
}