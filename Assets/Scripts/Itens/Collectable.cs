using UnityEngine;

/// <summary>
/// Behavior to enable the collection of this GameObject as a Collectable by a Collector.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
    /// <summary>
    /// The Collider2D that GameObject needs to know when has been collected by a Collector.
    /// </summary>
    private Collider2D coll2d;
    /// <summary>
    /// The list of effects that this Collectable has.
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
                effect.ApplyEffect(collector);
            }
            Destroy(gameObject);
        }
    }
}
