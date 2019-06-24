using UnityEngine;

public static class ModMath
{
    /// <summary>Implements true mathematical mod function for floats.</summary>
    /// <returns><paramref name="a"/> mod <paramref name="m"/>.</returns>
    /// <param name="a">a.</param>
    /// <param name="m">m.</param>
    public static float Mod(float a, float m)
    {
        return (a % m + m) % m;
    }

    /// <summary>Implements true mathematical mod function for integers.</summary>
    /// <returns><paramref name="a"/> mod <paramref name="m"/>.</returns>
    /// <param name="a">a.</param>
    /// <param name="m">m.</param>
    public static int Mod(int a, int m)
    {
        return (a % m + m) % m;
    }

    /// <summary>Calculates the smallest (in magnitude) float x such that <paramref name="a"/> + x = <paramref name="b"/> (mod <paramref name="m"/>).</summary>
    /// <returns>The signed modular distance.</returns>
    /// <param name="a">Number a.</param>
    /// <param name="b">Number b.</param>
    /// <param name="m">Modular parameter.</param>
    public static float SignedModularDistance(float a, float b, float m)
    {
        float delta1 = (b - a) % m;
        float delta2 = delta1 - Mathf.Sign(delta1) * m;

        return Mathf.Abs(delta1) <= Mathf.Abs(delta2) ? delta1 : delta2;
    }

    /// <summary>Calculates the smallest (in magnitude) vector v such that <paramref name="a"/> + v = <paramref name="b"/> (mod <paramref name="u"/>).</summary>
    /// <returns>The smallest delta vector.</returns>
    /// <param name="a">Point a.</param>
    /// <param name="b">Point b.</param>
    public static Vector2 MinDeltaVector(Vector2 a, Vector2 b, Vector2 u)
    {
        float deltaX = SignedModularDistance(a.x, b.x, u.x);
        float deltaY = SignedModularDistance(a.y, b.y, u.y);

        return new Vector2(deltaX, deltaY);
    }
}
