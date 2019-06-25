using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[RequireComponent(typeof(ChunkController))]
[RequireComponent(typeof(MapModel))]
public class Tiling : MonoBehaviour
{
    /***** Unity Params *****/

    /// <summary>The tile to be used.</summary>
    public Tile tile;


    /***** Unity Methods *****/

    void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        chunkController = GetComponent<ChunkController>();
        mapModel = GetComponent<MapModel>();
    }

    IEnumerator Start()
    {
        // Wait for Chunk Controller to be ready.
        yield return new WaitUntil(() => chunkController.isActiveAndEnabled);

        Tile(chunkController.ActiveChunks, new RectInt[] { });

        chunkController.onSwappingChunks.AddListener(Tile);
    }

    /// <summary>Generates tiles for in-editor tile visualization.</summary>
    [ContextMenu("Generate Tiles")]
    void GenerateTiles()
    {
        tilemap = GetComponent<Tilemap>();
        mapModel = GetComponent<MapModel>();
        regionColors.Clear();

        for (int x = 0; x < mapModel.mapSize; x++)
        {
            for (int y = 0; y < mapModel.mapSize; y++)
            {
                Vector2Int pos = new Vector2Int(x, y);
                Vector2Int center = Vector2Int.FloorToInt(mapModel.CenterOf(pos));
                if (!regionColors.TryGetValue(center, out Color color))
                {
                    color = Random.ColorHSV();
                    color.a = Mathf.Clamp01(1f / (center - pos).sqrMagnitude);
                    regionColors.Add(center, color);
                }

                tilemap.SetTile((Vector3Int)pos, tile);
                tilemap.SetTileFlags((Vector3Int)pos, TileFlags.InstantiateGameObjectRuntimeOnly | TileFlags.LockTransform);
                tilemap.SetColor((Vector3Int)pos, color);
            }
        }
    }

    /***** Internal *****/

    /// <summary>The tilemap.</summary>
    private Tilemap tilemap;

    /// <summary>The chunk controller.</summary>
    private ChunkController chunkController;

    /// <summary>The map model.</summary>
    private MapModel mapModel;

    /// <summary>Associates each region with a color.</summary>
    private readonly Dictionary<Vector2Int, Color> regionColors = new Dictionary<Vector2Int, Color>();

    /// <summary>Tile the map according to the chunks <paramref name="added"/> and <paramref name="removed"/>.</summary>
    /// <param name="added">Added.</param>
    /// <param name="removed">Removed.</param>
    private void Tile(IEnumerable<RectInt> added, IEnumerable<RectInt> removed)
    {
        foreach (RectInt chunk in added)
        {
            foreach (Vector2Int pos in chunk.allPositionsWithin)
            {
                Vector2Int center = Vector2Int.FloorToInt(mapModel.CenterOf(pos));
                if (!regionColors.TryGetValue(center, out Color color))
                {
                    color = Color.HSVToRGB(Random.value, 0.5f, 1f);
                    regionColors.Add(center, color);
                }

                tilemap.SetTile((Vector3Int)pos, tile);
                tilemap.SetTileFlags((Vector3Int)pos, TileFlags.InstantiateGameObjectRuntimeOnly | TileFlags.LockTransform);
                tilemap.SetColor((Vector3Int)pos, color);
            }
        }

        foreach (RectInt chunk in removed)
        {
            foreach (Vector2Int pos in chunk.allPositionsWithin)
            {
                tilemap.SetTile((Vector3Int)pos, null);
            }
        }
    }
}
