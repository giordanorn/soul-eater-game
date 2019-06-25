using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapModel : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>The length of the square grid.</summary>
    public int mapSize;

    /// <summary>The number of regions to be created.</summary>
    public int numberOfRegions;

    /// <summary>
    /// The spawner prefab.
    /// </summary>
    public CreatureSpawner enemySpawner;


    /***** Unity Methods *****/

    void Awake()
    {
        Partition(numberOfRegions);
    }

    [ContextMenu("Generate Partition")]
    void GeneratePartitions()
    {
        Partition(numberOfRegions);
    }

    /// <summary>Draw artifacts on the screen for in-editor partition visualization.</summary>
    void OnDrawGizmos()
    {
        foreach (Vector2 p in Centers)
        {
            Color prev = Gizmos.color;
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(p, 2);
            Gizmos.color = prev;
        }
    }


    /***** API *****/

    /// <summary>
    /// Gets the size of the map as a Vector2.
    /// </summary>
    /// <value>The size of the map.</value>
    public Vector2 MapSize => new Vector2(mapSize, mapSize);

    /// <summary>Gets an unmodifiable list of the enemy spawners.</summary>
    /// <value>The enemy spawners.</value>
    public IReadOnlyList<CreatureSpawner> Spawners => spawners;

    /// <summary>Generates the centers of the regions.</summary>
    /// <returns>The centers of the regions.</returns>
    public void Partition(int numberOfRegions)
    {
        Centers.Clear();

        for (int i = 0; i < numberOfRegions; i++)
        {
            // Generate random point inside grid
            float x = Random.Range(0, mapSize);
            float y = Random.Range(0, mapSize);

            Vector2 current = new Vector2(x, y);

            Centers.Add(current);
        }

        EqualizeCenterDistances();

        // if there is a spawner, add it to each region
        if (enemySpawner != null)
        {
            foreach (Vector2 center in Centers)
            {
                CreatureSpawner spawner = Instantiate(enemySpawner, center, Quaternion.identity);
                spawner.Map = this;
                spawner.transform.position = center;
                spawners.Add(spawner);
            }
        }
    }

    /// <summary>Gets the center of the region which <paramref name="point"/> belongs to.</summary>
    /// <returns>The center of the region containing <paramref name="point"/>.</returns>
    /// <param name="point">Point.</param>
    public Vector2 CenterOf(Vector2 point)
    {
        Vector2 mapPoint = InMapCoord(point);  
        return Centers.OrderBy(p => ModMath.MinDeltaVector(p, mapPoint, MapSize).sqrMagnitude).First();
    }

    /// <summary>
    /// Checks if the two points are in the same region.
    /// </summary>
    /// <returns><c>true</c>, if the points are in the same region, <c>false</c> otherwise.</returns>
    /// <param name="a">Point a.</param>
    /// <param name="b">Point b.</param>
    public bool SameRegion(Vector2 a, Vector2 b)
    {
        return Vector2Int.FloorToInt(CenterOf(a)) == Vector2Int.FloorToInt(CenterOf(b));
    }

    /// <summary>Gets the corresponding point in torus map coordinates.</summary>
    /// <returns>The point in torus map coordinate.</returns>
    /// <param name="point">Point.</param>
    public Vector2 InMapCoord(Vector2 point)
    {
        return new Vector2(ModMath.Mod(point.x, mapSize), ModMath.Mod(point.y, mapSize));
    }

    /***** Internal *****/


    /// <summary>Gets an unmodifiable list of the centers of the regions.</summary>
    /// <value>The centers of regions.</value>
    private List<Vector2> Centers { get; } = new List<Vector2>();

    /// <summary>
    /// The spawners.
    /// </summary>
    private List<CreatureSpawner> spawners = new List<CreatureSpawner>();

    /// <summary>Equalizes the distances between centers.</summary>
    private void EqualizeCenterDistances()
    {
        // Shorthand for the size of the centers array.
        int n = Centers.Count;

        // Simulation time step. 
        float t = Time.fixedDeltaTime;

        // Perform 25 iterations of a repulsion simulation.
        for (int _t = 0; _t < 25; _t++)
        {
            for (int i = 0; i < n; i++)
            {
                Vector2 totalRepulsion = Vector2.zero;

                // Calculate the total amount of repulsion by adding
                // all incident repelling forces.
                for (int j = 0; j < n; j++)
                {
                    // Calculates the distance between the two points.
                    Vector2 delta = ModMath.MinDeltaVector(Centers[i], Centers[j], MapSize);

                    // Normalize distance to unit torus.
                    delta.Scale(new Vector2(1f / mapSize, 1f / mapSize));

                    if (delta.sqrMagnitude > 0)
                    {
                        // Repulsion is proportional to the inverse of the square of the distance.
                        Vector2 displacement = -delta.normalized / delta.sqrMagnitude;

                        totalRepulsion += displacement;
                    }
                }

                // We want to calculate the displacement as a function of our time step.
                // Repulsion acts as a force on the current point.
                // Aceleration is proportional to the force: a = F/m;
                // Displacement is proportional to acceleration times t squared:
                // Δx = v * t = a * t * t
                // Since t is small, we will use t instead of t^2 to make each step more meaningful.
                Centers[i] += totalRepulsion * t;

                // Ensure point remains inside torus.
                float x = (Centers[i].x % mapSize + mapSize) % mapSize;
                float y = (Centers[i].y % mapSize + mapSize) % mapSize;

                Centers[i] = new Vector2(x, y);
            }
        }
    }
}
