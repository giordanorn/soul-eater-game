using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CauseDamageOnCollision : MonoBehaviour
{
    public int damage = 0;
    public string target_tag = "Player";

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Creature creature = other.gameObject.GetComponent<Creature>();
        if (creature != null && other.CompareTag(target_tag))
        {
            creature.TakeDamage(damage);
        }
    }
}
