using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Tiling : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>The size of the grid along the x axis.</summary>
    public int xSize = 100;

    /// <summary>The size of the grid along the y axis.</summary>
    public int ySize = 100;

    /// <summary>The tiles to be used.</summary>
    public Tile[] tiles;


    /***** Internal *****/

    /// <summary>The tilemap component</summary>
    private Tilemap tilemap;

    /// <summary>The partitioning component.</summary>
    private Partitioning partitioning;


    /***** API *****/

    /// <summary>Tile this instance.</summary>
    [ContextMenu("Generate Tiles")]
    public void Tile()
    {
        // If there are no tiles, there is nothing to do.
        if (tiles == null || tiles.Length == 0)
            return;

        tilemap = GetComponent<Tilemap>();
        partitioning = GetComponent<Partitioning>();

        // Associate each region with a color.
        IDictionary<Vector3Int, Color> regionColors = new Dictionary<Vector3Int, Color>();

        // Iterate over all possible grid positions.
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                // The current position in the grid.
                Vector3Int tilePos = new Vector3Int(x, y, 0);

                // Get a random tile from the tile palette.
                int tileIndex = Random.Range(0, tiles.Length);

                // Set the tile in the current grid position. 
                tilemap.SetTile(tilePos, tiles[tileIndex]);

                // Check if tilemap has partitions.
                if (partitioning != null && partitioning.RegionCenters.Count > 0)
                {
                    // Remove color lock for this tile.
                    tilemap.SetTileFlags(tilePos, TileFlags.InstantiateGameObjectRuntimeOnly | TileFlags.LockTransform);

                    // Figure out which region this tile belongs to.
                    Vector3Int center = Vector3Int.RoundToInt(partitioning.CenterOf(tilePos));

                    // If this region has no color associated, assign a random one to it.
                    if (!regionColors.TryGetValue(center, out Color color))
                    {
                        color = Random.ColorHSV();
                        regionColors.Add(center, color);
                    }

                    // Set the color for this tile.
                    tilemap.SetColor(tilePos, color);
                }
            }
        }
    }


}
