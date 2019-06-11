using System.Collections.Generic;
using UnityEngine;

static class ExtensionMethods
{
    /// <summary>
    /// Rounds Vector3.
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="decimalPlaces"></param>
    /// <returns></returns>
    public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }

        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }

    /// <summary>
    /// Ceil Vector3.
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>
    public static Vector3 Ceil(this Vector3 vector3)
    {
        return new Vector3(
            Mathf.Ceil(vector3.x),
            Mathf.Ceil(vector3.y),
            Mathf.Ceil(vector3.z));
    }

    /// <summary>
    /// Floor Vector3.
    /// </summary>
    /// <param name="vector3"></param>
    /// <returns></returns>F
    public static Vector3 Floor(this Vector3 vector3)
    {
        return new Vector3(
            Mathf.Floor(vector3.x),
            Mathf.Floor(vector3.y),
            Mathf.Floor(vector3.z));
    }

    static List<GameObject> _ddolObjects = new List<GameObject>();

    public static void DontDestroyOnLoad(this GameObject go)
    {
        UnityEngine.Object.DontDestroyOnLoad(go);
        _ddolObjects.Add(go);
    }

    public static void DestroyAll()
    {
        foreach (var go in _ddolObjects)
            if (go != null)
                UnityEngine.Object.Destroy(go);

        _ddolObjects.Clear();
    }
}