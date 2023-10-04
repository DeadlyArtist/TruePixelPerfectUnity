using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
[DisallowMultipleComponent]
[AddComponentMenu("Rendering/2D/True Pixel Perfect")]
[RequireComponent(typeof(Camera))]
public class TruePixelPerfect : MonoBehaviour
{
	public int spritePixelsPerUnit = 32;
	public int maxVerticalSpritePixels = 256;

	private int pixelsPerSpritePixel = 4;

	// Inspector Only
	private float unevenPixelHeightOffset = 0;
	private float unevenPixelWidthOffset = 0;
	private Rect pixelRect;

	private Camera r_camera;

    	void Awake()
    	{
		r_camera = GetComponent<Camera>();
		UpdateCamera();
	}

	void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
	{
		if (camera == r_camera)
		{
			UpdateCamera();
		}
	}

	void OnEnable()
	{
		RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
	}

	void OnDisable()
	{
		RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;

		transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
		unevenPixelHeightOffset = 0;
		unevenPixelWidthOffset = 0;

		r_camera.rect = new Rect(0, 0, 1, 1);
		r_camera.ResetWorldToCameraMatrix();
	}

	void UpdateCamera()
    	{
		var pixelRect = r_camera.pixelRect;
		this.pixelRect = pixelRect;

		int pixelsPerSpritePixel = (int)Mathf.Ceil(pixelRect.height / maxVerticalSpritePixels);
		this.pixelsPerSpritePixel = pixelsPerSpritePixel;

		float ortographicSize = pixelRect.height / (pixelsPerSpritePixel * spritePixelsPerUnit);
		r_camera.orthographicSize = ortographicSize / 2;

		var newPosition = new Vector3(0, 0, transform.localPosition.z);
		if (pixelRect.height % 2 != 0)
		{
			float unevenPixelHeightOffset = 1f / (pixelsPerSpritePixel * spritePixelsPerUnit * 2);
			this.unevenPixelHeightOffset = unevenPixelHeightOffset;

			newPosition += new Vector3(0, unevenPixelHeightOffset, 0);
		}
		else
		{
			this.unevenPixelHeightOffset = 0;
		}
		if (pixelRect.width % 2 != 0)
		{
			float unevenPixelWidthOffset = 1f / (pixelsPerSpritePixel * spritePixelsPerUnit * 2);
			this.unevenPixelWidthOffset = unevenPixelWidthOffset;

			newPosition += new Vector3(unevenPixelWidthOffset, 0, 0);
		}
		else
		{
			this.unevenPixelWidthOffset = 0;
		}

		transform.localPosition = newPosition;
	}
}
