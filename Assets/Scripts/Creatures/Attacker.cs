using UnityEngine;

[RequireComponent(typeof(Creature))]
public class Attacker : MonoBehaviour
{
    public float strengh = 0;

    public void Hit(Creature target)
    {
        target.TakeDamage(strengh);
    }
}
