using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class ProceduralGeneration : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>The size of the map along the x axis.</summary>
    public int xSize = 100;

    /// <summary>The size of the map along the y axis.</summary>
    public int ySize = 100;

    /// <summary>The minimum radius allowed for a region.</summary>
    public float minRadius;

    /// <summary>The tiles to be used.</summary>
    public Tile[] tiles;

    /// <summary>Max number of regions to generate.</summary>
    public int maxNumberOfRegions = 10;

    /// <summary>Max number of attempts to generate new regions with the given restrictions.</summary>
    public int maxTries = 5;


    /***** Unity Components *****/

    /// <summary>The tilemap.</summary>
    private Tilemap tilemap;


    /***** Unity Methods *****/

    /// <summary>Generates map.</summary>
    [ContextMenu("Generate map")]
    void GenerateMap()
    {
        tilemap = tilemap ?? GetComponent<Tilemap>(); 

        GenerateRegions(GenerateCenters());
    }

    /***** Private Methods *****/

    /// <summary>Generates the centers of the regions.</summary>
    /// <returns>The centers of the regions.</returns> 
    private IList<Vector3Int> GenerateCenters()
    {
        // List of generated region centers.
        IList<Vector3Int> centers = new List<Vector3Int>();

        for (int i = 0; i < maxNumberOfRegions; i++)
        {
            // Generate random point in grid.
            int x = Random.Range(0, xSize);
            int y = Random.Range(0, ySize);

            // Translation performed to create map around the midpoint of the tilemap.
            Vector3Int current = new Vector3Int(x - xSize / 2, y - ySize / 2, 0);

            // Keep track of how many candidate points we have rejected.
            int tryCount = 0;

            // If this point is too close to another which is a region center, try another.
            while (centers.Any(p => (p - current).sqrMagnitude <= minRadius * minRadius))
            {
                x = Random.Range(0, xSize);
                y = Random.Range(0, ySize);

                current.x = x - xSize / 2;
                current.y = y - ySize / 2;

                tryCount++;

                // If we have rejected too many points, stop trying and return what we have.
                if (tryCount == maxTries)
                {
                    return centers;
                }
            }

            centers.Add(current);
        }

        return centers;
    }

    /// <summary>Generates the regions, given the centers.</summary>
    /// <param name="centers">Centers of the regions.</param>
    private void GenerateRegions(IList<Vector3Int> centers)
    {
        // Checking preconditions.
        if (centers == null)
        {
            Debug.Log("No centers! (List is null)");
            return;
        }

        // Checking preconditions.
        if (centers.Count == 0)
        {
            Debug.Log("No centers! (List is empty)");
            return;
        }

        // Dictionary associating each region center with the index of the tile it should be filled with.
        IDictionary<Vector3Int, int> regionTiles = new Dictionary<Vector3Int, int>();

        // Randomly choose a tile for each region.
        foreach (Vector3Int p in centers)
        {
            regionTiles[p] = Random.Range(0, tiles.Length);
        }

        // Fill the map using Voronoi cells.
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                // The current point.
                Vector3Int current = new Vector3Int(x - xSize / 2, y - ySize / 2, 0);

                // Calculate which center is the closest to the current point.
                Vector3Int closest = centers.OrderBy(p => Distance(current, p)).First();

                // Set the tile for this point accordingly.
                tilemap.SetTile(current, tiles[regionTiles[closest]]);
            }
        }
    }

    /// <summary>Calculates the distance between points <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <returns>The distance between <paramref name="a"/> and <paramref name="b"/>.</returns>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    private int Distance(Vector3Int a, Vector3Int b)
    {
        return (a - b).sqrMagnitude;
    }
}
