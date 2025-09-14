# TruePixelPerfectUnity
A true pixel perfect camera implementation for unity.

Features:
- Allows seemlessly switching between pixel perfect and normal
- Handles GUI separately
- Allows rescaling GUI based on user defined settings
- Shows subpixels for maximum smoothness of movement
- Allows freely resizing the screen without any cropping or errors (Offsets the camera in case of uneven pixel counts)
- Big API full of convenience

## How To Use
1. Create a "Singletons" gameobject
1. Add the PixelPerfect component (and GameSettings, unless you already have a replacement, which is recommended)
1. Locate your camera
1. Set "Projection" setting of your camera to "Orthographic"
1. Add the CameraUpdater component to your camera (and optionally add the SnapToGrid component to its dedicated parent)
1. Create a dedicated parent for your Camera and add the SnapToGrid component to its dedicated parent
1. Set "Sprite Pixels Per Unit" to the "Pixels Per Unit" setting of your sprite assets
1. Set "Max Vertical Sprite Pixels" to how many pixels should at maximum be on the screen vertically
1. Add the CanvasScaler and CanvasScalerUpdater components to all your canvases

## Help
- Make sure all sprites which should snap to pixels have a SnapToGrid component on their dedicated parent
- Make sure all moving rigidbodies are set to interpolate
- Make sure Physics2D simulation mode in General Settings (Project Settings) is set to FixedUpdate, and that all rigidbody moving is done in FixedUpdate
- Make sure you are not directly moving the camera or any object with the SnapToGrid component
- Enable GridSnapping in Scene view and set it to 1 / "Sprite Pixels Per Unit"
- If movement is stuttering, it could be because rigidbody is selected in Scene view. Minimal stuttering can also be caused by the editor and go away in the build.
- Cinemachine, inbuilt PixelPerfectCamera, and anything else that modifies camera position or orthogonal size are incompatible with this
