# TruePixelPerfectUnity
A true pixel perfect camera implementation for unity.

## How To Use
- Add the "True Pixel Perfect" component to your camera.
- Set "Sprite Pixels Per Unit" to the "Pixels Per Unit" setting of your sprite assets.
- Set "Max Vertical Sprite Pixels" to how many pixels should at maximum be on the screen vertically.

### Important
- This script modifies the position, so it is advised to not directly move the camera, instead only move it indirectly via a parent.
- If you don't move all objects including the camera (parent) to valid positions (multiples of 1 / spritePixelsPerUnit) at all times, this doesn't work.
- Cinemachine, inbuilt PixelPerfectCamera and anything else that modifies position or orthogonal size are incompatible with this.
