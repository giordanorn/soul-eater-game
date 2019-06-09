using UnityEngine;

/// <summary>
/// Behaviour to collect Collectibles and if a be a Creature, inform this fact.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Collector : MonoBehaviour
{
    /// <summary>
    /// The Collider2D that GameObject needs to know when to collect a Collectible.
    /// </summary>
    private Collider2D coll2d;

    /// <summary>
    ///  The Creature Behaviour if the GameObject has one.
    /// </summary>
    private Creature creature;

    private void Awake()
    {
        coll2d = GetComponent<Collider2D>();
        creature = GetComponent<Creature>();
        coll2d.isTrigger = false;
    }

    /// <summary>
    /// Informs if this Collector is a Creature.
    /// </summary>
    /// <returns>True if is a Creature, False otherwise.</returns>
    public bool IsCreature()
    {
        return creature != null;
    }
}
