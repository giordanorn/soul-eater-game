using UnityEngine;


[RequireComponent(typeof(Creature))]
public class ImmunityAfterTakeDamage : MonoBehaviour
{
    public float immunitySeconds;
    private float currentCooldown;

    void Start()
    {
        currentCooldown = 0.0f;
    }

    void Update()
    {
        if (InCooldown())
        {
            DiscountCooldown();
        }
    }

    public bool InCooldown()
    {
        return currentCooldown != 0.0f;
    }

    private void DiscountCooldown()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0.0f)
            currentCooldown = 0.0f;
    }

    public void ResetCooldown()
    {
        currentCooldown = immunitySeconds;
    }
}
