using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collector : MonoBehaviour
{
    private Collider2D coll2d;

    private Creature creature;

    private void Awake()
    {
        coll2d = GetComponent<Collider2D>();
        creature = GetComponent<Creature>();
        coll2d.isTrigger = false;
    }

    public bool IsCreature()
    {
        return creature != null;
    }
}
