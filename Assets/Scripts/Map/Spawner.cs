using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>The prefab to be spawned.</summary>
    public GameObject prefab;


    /***** Unity Methods *****/

    void Awake()
    {
        spawned = new HashSet<GameObject>();
    }

    void Update()
    {
        if (totalSpawned >= MaxSpawnedTotal)
        {
            Destroy(gameObject);
            return;
        }

        if (spawned.Count >= MaxSpawnedWave)
        {
            return;
        }

        if (lastSpawned < SpawnCooldown)
        {
            lastSpawned += Time.deltaTime;
            return;
        }

        Spawn();

        lastSpawned = 0f;
        totalSpawned++;
    }


    /***** API *****/

    /// <summary>Gets or sets the maximum number of objects to be spawned in total.</summary>
    /// <value>The maximum number of objects to be spawned in total.</value>
    public int MaxSpawnedTotal { get; set; }

    /// <summary>Gets or sets the maximum number of spawned objects at a time (i.e. in a wave).</summary>
    /// <value>The maximum number of spawned objects at a time.</value>
    public int MaxSpawnedWave { get; set; }

    /// <summary>Gets or sets the cooldown between spawns.</summary>
    /// <value>The cooldown between spawns.</value>
    public float SpawnCooldown { get; set; } = 1f;


    /***** Internal *****/

    /// <summary>Spawn an instance of the prefab.</summary>
    private void Spawn()
    {
        spawned.Add(Instantiate(prefab, transform));
    }

    /// <summary>Despawn the specified instance.</summary>
    /// <param name="instance">Instance to despawn.</param>
    private void Despawn(GameObject instance)
    {
        spawned.Remove(instance);
    }

    /// <summary>The number of instances spawned in total.</summary>
    private int totalSpawned;

    /// <summary>The time in seconds since the last spawn.</summary>
    private float lastSpawned;

    /// <summary>The spawned instances.</summary>
    private ISet<GameObject> spawned;
}
