using UnityEngine;

/// <summary>
/// Behavior to enable the collection of this GameObject as a Collectible by a Collector.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    /// <summary>
    /// The Collider2D that GameObject needs to know when has been collected by a Collector.
    /// </summary>
    private Collider2D coll2d;
    /// <summary>
    /// The list of effects that this Collectible has.
    /// </summary>
    public Effect[] effects;

    private void Awake()
    {
        coll2d = GetComponent<Collider2D>();
        effects = GetComponents<Effect>();
        coll2d.isTrigger = true;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Collector collector = col.GetComponent<Collector>();
        if (collector != null)
        {
            foreach (Effect effect in effects)
            {
                effect.Do(collector);
            }
            Destroy(gameObject);
        }
    }
}
