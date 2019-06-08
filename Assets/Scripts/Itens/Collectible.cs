using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    private Collider2D coll2d;
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
