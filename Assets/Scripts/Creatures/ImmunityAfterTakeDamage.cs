using UnityEngine;


[RequireComponent(typeof(Creature))]
public class ImmunityAfterTakeDamage : Cooldown
{
    public float immunitySeconds;
    private float currentCooldown;

    public void ResetCooldown()
    {
        StartCooldown(immunitySeconds);
    }
}
