using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Behaviour to collect Collectibles and if a be a Creature, inform this fact.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Collector : MonoBehaviour
{
    private UnityEvent soulCollected;


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
        soulCollected = new UnityEvent();
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

    public void addActionOnSoulCollected(UnityAction function)
    {
        soulCollected.AddListener(function);
    }

    public void Collect(Effect[] effects, string tag)
    {
        if (tag == "Soul")
        {
            soulCollected.Invoke();
        }
        foreach (Effect effect in effects)
        {
            effect.ApplyEffect(gameObject);
        }
    }
}
