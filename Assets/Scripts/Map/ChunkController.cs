using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MapModel))]
public class ChunkController : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>The object around which the chunks should be generated.</summary>
    public GameObject pivot;

    /// <summary>The radius of the active chunk grid.</summary>
    public int activeRadius;

    /// <summary>The chunk side length.</summary>
    public int chunkSize;


    /***** Unity Methods *****/

    void Awake()
    {
        mapModel = GetComponent<MapModel>();

        chunkComparer = new ChunkEqualityComparer(mapModel);
    }

    void Start()
    {
        // Chunks per side is rounded up to cover every point in the map. 
        chunksPerSide = Mathf.CeilToInt((float)mapModel.mapSize / chunkSize);

        activeChunks = new HashSet<RectInt>(
                            NeighboringChunks(pivot.transform.position),
                            chunkComparer
                            );

        centralChunk = GlobalContainingChunk(pivot.transform.position);

        foreach (CreatureSpawner spawner in mapModel.Spawners)
        {
            spawner.ChunkController = this;
        }

        onSwappingChunks.Invoke(null, null);
    }


    void Update()
    {
        // Checks if the pivot has gone too far.
        Vector2 delta = (Vector2)pivot.transform.position - centralChunk.center;
        float radiusInTiles = activeRadius * chunkSize;
        if (delta.sqrMagnitude > radiusInTiles * radiusInTiles / 4)
        {
            // If it did, update chunks and fire event.

            // Recalculate chunks.
            IEnumerable<RectInt> nbChunks = NeighboringChunks(pivot.transform.position);

            // Preparing modification lists for event.
            IEnumerable<RectInt> added = nbChunks.Except(activeChunks, chunkComparer).ToArray();
            IEnumerable<RectInt> removed = activeChunks.Except(nbChunks, chunkComparer).ToArray();

            // Update active chunks.
            activeChunks.ExceptWith(removed);
            activeChunks.UnionWith(added);

            // Update central chunk.
            centralChunk = GlobalContainingChunk(pivot.transform.position);

            // Fire event.
            onSwappingChunks.Invoke(added, removed);
        }
    }


    /***** API *****/

    /// <summary>Gets the currently active chunks.</summary>
    /// <value>The currently active chunks.</value>
    public IReadOnlyCollection<RectInt> ActiveChunks => activeChunks;

    /// <summary>Event that fires when swapping chunks.</summary>
    public readonly UnityEvent<IEnumerable<RectInt>, IEnumerable<RectInt>> onSwappingChunks = new SwappingChunksEvent();

    /// <summary>
    /// Checks if <paramref name="position"/> is in an active chunk, in which 
    /// case <paramref name="activePosition"/> will be the corresponding position
    /// in the active chunk.
    /// </summary>
    /// <returns><c>true</c>, if <paramref name="position"/> is in an active chunk, <c>false</c> otherwise.</returns>
    /// <param name="position">Position.</param>
    /// <param name="activePosition">Corresponding position in the active chunk.</param>
    public bool IsInActiveChunk(Vector2 position, out Vector2 activePosition)
    {
        // Get reduced corresponding coordinate inside map bounds.
        Vector2 mapPos = mapModel.InMapCoord(position);

        // Checks if any of the currently active chunks contains the position
        RectInt chunk = GlobalContainingChunk(position);
        foreach (RectInt rect in activeChunks)
        {
            if (chunkComparer.Equals(rect, chunk))
            {
                activePosition = position - chunk.min + rect.min;
                return true;
            }
        }

        activePosition = default;
        return false;
    }


    /***** Internal *****/

    /// <summary>The number of chunks per side.</summary>
    private int chunksPerSide;

    /// <summary>The chunks which are currently active.</summary>
    private HashSet<RectInt> activeChunks;

    /// <summary>The central chunk.</summary>
    private RectInt centralChunk;

    /// <summary>The equality comparer for chunks.</summary>
    private ChunkEqualityComparer chunkComparer;

    /// <summary>The map model.</summary>
    private MapModel mapModel;

    /// <summary>
    /// Gets the world space chunk containing <paramref name="position"/>.
    /// </summary>
    /// <returns>The world space chunk containing <paramref name="position"/>.</returns>
    /// <param name="position">Position.</param>
    private RectInt GlobalContainingChunk(Vector2 position)
    {
        int i = Mathf.FloorToInt(position.x / chunkSize);
        int j = Mathf.FloorToInt(position.y / chunkSize);

        // Chunk starts at (xMin, yMin), and ends at (xMin + width - 1, yMin + height - 1).
        return new RectInt(
            xMin: i * chunkSize,
            yMin: j * chunkSize,
            width: chunkSize,
            height: chunkSize
            );
    }

    /// <summary>Gets the Moore neighboorhood of the chunk containing <paramref name="position"/>.</summary>
    /// <returns>The chunks.</returns>
    /// <param name="position">Position.</param>
    private IEnumerable<RectInt> NeighboringChunks(Vector2 position)
    {
        List<Vector2> chunkRepresentants = new List<Vector2>((2 * activeRadius + 1) * (2 * activeRadius + 1));

        // Selects a point from the corresponding cells in the unit grid. 
        for (int i = -activeRadius; i <= activeRadius; i++)
        {
            for (int j = -activeRadius; j <= activeRadius; j++)
            {
                chunkRepresentants.Add(Vector2.right * i + Vector2.down * j);
            }
        }

        // Scale and translate the grid, retrieving the required neighbors.
        foreach (Vector2 dir in chunkRepresentants)
        {
            yield return GlobalContainingChunk(position + chunkSize * dir);
        }
    }
}
