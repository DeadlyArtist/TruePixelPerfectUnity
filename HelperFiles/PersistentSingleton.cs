using UnityEngine;

/// <summary>
/// A base class implementing a persistent singleton pattern for MonoBehaviour components.
/// Any instance beyond the first self destructs.
/// </summary>
public class PersistentSingleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;

    /// <summary>
    /// Do NOT compare this to null, use IsInstanceNull instead. Otherwise a New Game Object will be created and not cleaned up.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null) _instance = FindAnyObjectByType<T>();
            return _instance;
        }
    }

    /// <summary>
    /// Use this to check if the instance is null
    /// </summary>
    public static bool IsInstanceNull => _instance == null;

    protected virtual void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (this != _instance) Destroy(gameObject); // Self destruction if an instance already exists
        }
    }
}