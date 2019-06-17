using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Tiling))]
public class Partitioning : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>The number of regions to be created.</summary>
    public int numberOfRegions = 10;


    /***** Internal *****/

    /// <summary>The tiling component.</summary>
    private Tiling tiling;

    /// <summary>Gets the size of the grid along the x axis.</summary>
    /// <value>Grid size along x axis.</value>
    private int XSize => tiling.xSize;

    /// <summary>Gets the size of the grid along the y axis.</summary>
    /// <value>Grid size along y axis.</value>
    private int YSize => tiling.ySize;

    // <summary>List of generated region centers.</summary>
    private List<Vector3> centers = new List<Vector3>();


    /***** API *****/

    /// <summary>Gets an unmodifiable list of the centers of the regions.</summary>
    /// <value>The centers of regions.</value>
    public IReadOnlyList<Vector3> RegionCenters => centers;

    /// <summary>Generates the centers of the regions.</summary>
    /// <returns>The centers of the regions.</returns>
    public void Partition()
    {
        tiling = tiling ?? GetComponent<Tiling>();

        centers.Clear();

        for (int i = 0; i < numberOfRegions; i++)
        {
            float x = Random.Range(0, XSize);
            float y = Random.Range(0, YSize);

            Vector3 current = new Vector3(x, y, 0);

            centers.Add(current);
        }

        EqualizeCenterDistances();
    }

    /// <summary>Equalizes the distances between centers.</summary>
    private void EqualizeCenterDistances()
    {
        int n = centers.Count;

        // Perform 25 iterations of a repulsion simulation.
        for (int _t = 0; _t < 25; _t++)
        {
            for (int i = 0; i < n; i++)
            {
                Vector3 totalRepulsion = Vector3.zero;

                // Calculate the total amount of repulsion by adding
                // all incident repelling forces.
                for (int j = 0; j < n; j++)
                {
                    // Calculates the distance between the two points.
                    Vector3 delta = ToroidalDelta(centers[i], centers[j]);

                    // Normalize distance to unit torus.
                    delta.Scale(new Vector3(1f / XSize, 1f / YSize, 0));

                    if (delta.sqrMagnitude > 0)
                    {
                        // Repulsion is proportional to the inverse of the square of the distance.
                        Vector3 displacement = -Vector3.Normalize(delta) / delta.sqrMagnitude;

                        totalRepulsion += displacement;
                    }
                }

                // Perform a step of the simulation for this point.
                centers[i] += totalRepulsion * Time.fixedDeltaTime;

                // Ensure point remains inside torus.
                float x = (centers[i].x % XSize + XSize) % XSize;
                float y = (centers[i].y % YSize + YSize) % YSize;
                float z = centers[i].z;

                centers[i] = new Vector3(x, y, z);
            }
        }
    }

    /// <summary>Calculates the signed modular distance between numbers <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <returns>The signed modular distance.</returns>
    /// <param name="a">Number a.</param>
    /// <param name="b">Number b.</param>
    /// <param name="m">Modular parameter.</param>
    private float SignedModularDistance(float a, float b, int m)
    {
        float delta1 = (b - a) % m;
        float delta2 = delta1 - Mathf.Sign(delta1) * m;

        return Mathf.Abs(delta1) <= Mathf.Abs(delta2) ? delta1 : delta2;
    }

    /// <summary>Calculates the toroidal distance between points <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <returns>The toroidal distance.</returns>
    /// <param name="a">Point a.</param>
    /// <param name="b">Point b.</param>
    private Vector3 ToroidalDelta(Vector3 a, Vector3 b)
    {
        float deltaX = SignedModularDistance(a.x, b.x, tiling.xSize);
        float deltaY = SignedModularDistance(a.y, b.y, tiling.ySize);

        return new Vector3(deltaX, deltaY, 0);
    }

    /// <summary>Gets the center of the region which <paramref name="point"/> belongs to.</summary>
    /// <returns>The center of the region containing <paramref name="point"/>.</returns>
    /// <param name="point">Point.</param>
    public Vector3 CenterOf(Vector3 point)
    {
        return centers.OrderBy(p => ToroidalDelta(p, point).sqrMagnitude).First();
    }


    /***** Unity Methods *****/

    [ContextMenu("Generate Partition")]
    void GeneratePartition()
    {
        Partition();
    }

    void OnDrawGizmos()
    {
        foreach (Vector3 p in centers)
        {
            Color prev = Gizmos.color;
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(p, 1);
            Gizmos.color = prev;
        }
    }
}
