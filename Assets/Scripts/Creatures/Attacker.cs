using UnityEngine;


[RequireComponent(typeof(Creature))]
public class Attacker : MonoBehaviour
{
    public int strengh = 0;

    public Creature target;

    public KeyCode hitKey = KeyCode.Space;

    void Update()
    {
        if (Input.GetKeyDown(hitKey))
        {
            Hit();
        }
    }


    public void Hit()
    {
        target.TakeDamage(strengh);
    }
}
