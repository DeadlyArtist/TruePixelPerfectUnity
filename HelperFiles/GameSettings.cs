using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// This is just an example. Your game will likely have its own settings system.
public class GameSettings : PersistentSingleton<GameSettings>
{
    [SerializeField] private bool pixelPerfect = true;
    public static bool PixelPerfect => Instance.pixelPerfect;

    [SerializeField] private bool pixelPerfectGUI = true;
    public static bool PixelPerfectGUI => Instance.pixelPerfectGUI;

    [SerializeField] private int targetGUISizeReduction = 2;
    public static int TargetGUISizeReduction => Instance.targetGUISizeReduction;

    [Range(0.1f, 1f)]
    [SerializeField] private float guiScale = 1f;
    public static float GuiScale => Instance.guiScale;
}