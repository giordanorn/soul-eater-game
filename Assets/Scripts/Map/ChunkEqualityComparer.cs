using System.Collections.Generic;
using UnityEngine;

/// <summary>Chunk equality comparer. Compares two chunks by their positions on the given map.
/// Assumes all chunks have the same size, and performs best when there are at most 2^16 chunks
/// per side.</summary>
public class ChunkEqualityComparer : IEqualityComparer<RectInt>
{
    /***** API *****/

    /// <summary>Instantiates a <see cref="T:ChunkEqualityComparer"/>.</summary>
    /// <param name="mapModel">Map model.</param>
    public ChunkEqualityComparer(MapModel mapModel)
    {
        this.mapModel = mapModel;
    }

    /// <summary>Checks if <paramref name="r1"/> and <paramref name="r2"/> are equal.</summary>
    /// <returns>true, if equal, false otherwise.</returns>
    /// <param name="r1">r1.</param>
    /// <param name="r2">r2.</param>
    public bool Equals(RectInt r1, RectInt r2)
    {
        return ModMath.Mod(r1.xMin, MapSize) == ModMath.Mod(r2.xMin, MapSize)
            && ModMath.Mod(r1.yMin, MapSize) == ModMath.Mod(r2.yMin, MapSize);
    }

    /// <summary>Generates a hash code for a chunk.</summary>
    /// <returns>The hash code.</returns>
    /// <param name="rect">Rect.</param>
    public int GetHashCode(RectInt rect)
    {
        // Use chunk indexes for hashing.
        int i = ModMath.Mod(rect.xMin / rect.width, MapSize);
        int j = ModMath.Mod(rect.yMin / rect.height, MapSize);

        // Assuming there are less than 2^16 chunks in each side,
        // the number below is unique for each chunk.
        int x = (i << 16) + j;

        // Apply function described here:
        // https://ticki.github.io/blog/designing-a-good-non-cryptographic-hash-function/
        x += 1;
        x ^= x >> 11;
        x *= 3217;
        x ^= x >> 11;
        x *= 3217;
        x ^= x >> 11;

        return x;
    }


    /***** Internal *****/

    /// <summary>The map model.</summary>
    private readonly MapModel mapModel;

    /// <summary>Gets the size of the map.</summary>
    /// <value>The size of the map.</value>
    private int MapSize => mapModel.mapSize;
}
