using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreatureSpawner : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>The prefabs to be spawned.</summary>
    public Creature[] prefabs;

    /// <summary>Gets or sets the maximum number of objects to be spawned in total.</summary>
    /// <value>The maximum number of objects to be spawned in total.</value>
    public int maxSpawnedTotal = 100;

    /// <summary>Gets or sets the maximum number of spawned objects at a time (i.e. in a wave).</summary>
    /// <value>The maximum number of spawned objects at a time.</value>
    public int maxSpawnedWave = 15;

    /// <summary>Gets or sets the cooldown between spawns.</summary>
    /// <value>The cooldown between spawns.</value>
    public float spawnCooldown = 1f;


    /***** Unity Methods *****/

    void Awake()
    {
        spawned = new HashSet<Creature>();
    }

    void Update()
    {
        if (totalSpawned >= maxSpawnedTotal)
        {
            enabled = false;
            return;
        }

        if (spawned.Count >= maxSpawnedWave)
        {
            return;
        }

        if (lastSpawned < spawnCooldown)
        {
            lastSpawned += Time.deltaTime;
            return;
        }

        Spawn();

        lastSpawned = 0f;
        totalSpawned++;
    }


    /***** API *****/

    /// <summary>
    /// Gets or sets the function to select a creature
    /// </summary>
    /// <value>The function to select a creature.</value>
    public Func<Creature[], Creature> SelectCreature { get; set; } =
        (prefabs) =>
        {
            float t = Random.Range(0f, prefabs.Length-1);
            return prefabs[Mathf.RoundToInt(t * t)];
        };

    /// <summary>
    /// Sets the chunk controller.
    /// </summary>
    /// <value>The chunk controller.</value>
    public ChunkController ChunkController
    {
        private get => chunkController;

        set
        {
            if (chunkController == null)
            {
                chunkController = value;
                chunkController.onSwappingChunks.AddListener(OnSwappingChunks);
            }
        }
    }

    /// <summary>
    /// Sets the map.
    /// </summary>
    /// <value>The map.</value>
    public MapModel Map
    {
        private get => map;

        set
        {
            if (map == null)
            {
                map = value;
            }
        }
    }


    /***** Internal *****/

    /// <summary>The number of instances spawned in total.</summary>
    private int totalSpawned;

    /// <summary>The time in seconds since the last spawn.</summary>
    private float lastSpawned;

    /// <summary>The spawned instances.</summary>
    private ISet<Creature> spawned;

    /// <summary>The map.</summary>
    private MapModel map;

    /// <summary>The chunk controller.</summary>
    private ChunkController chunkController;

    /// <summary>Spawn an instance of the prefab.</summary>
    private void Spawn()
    {
        Creature selected = SelectCreature(prefabs);
        Creature creature = Instantiate(selected, transform);
        creature.Map = Map;
        creature.addActionOnDeath(() => Despawn(creature));
        spawned.Add(creature);

        Vector2 point;
        float radius = Map.mapSize/Map.numberOfRegions;
        do
        {
            point = transform.position + (Vector3)Random.insideUnitCircle * radius;
            radius /= 1.25f;
        }
        while (!Map.SameRegion(point, transform.position));

        creature.transform.position = point;
    }

    /// <summary>Despawn the specified instance.</summary>
    /// <param name="instance">Instance to despawn.</param>
    private void Despawn(Creature instance)
    {
        spawned.Remove(instance);
    }

    /// <summary>Method to be executed when swapping chunks.</summary>
    /// <param name="added">Added chunks.</param>
    /// <param name="removed">Removed chunks.</param>
    private void OnSwappingChunks(IEnumerable<RectInt> added, IEnumerable<RectInt> removed)
    {
        foreach (Creature creature in spawned)
        {
            SpriteRenderer spriteRenderer = creature.GetComponent<SpriteRenderer>();
            if (chunkController.IsInActiveChunk(creature.transform.position, out Vector2 activePosition))
            {
                spriteRenderer.enabled = true;
                creature.transform.position = activePosition;
            }
            else
            {
                spriteRenderer.enabled = false;
            }
        }
    }
}
