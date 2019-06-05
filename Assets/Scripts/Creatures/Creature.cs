using UnityEngine;

public class Creature : MonoBehaviour
{
    private HealthReserve healthReserve;
    private ImmunityAfterTakeDamage immunity;

    void Start()
    {
        healthReserve = GetComponent<HealthReserve>();
        immunity = GetComponent<ImmunityAfterTakeDamage>();
    }

    void  Update()
    {
        if (healthReserve.IsEmpty())
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        if (!IsImmune())
        {
            if (HasHealthReserve())
            {
                healthReserve.Decrease(damage);
            }
            if (HasDamageCooldown())
            {
                immunity.ResetCooldown();
            }
        }
    }
    
    public void Kill()
    {
        Die();
    }
    
    private bool IsImmune()
    {
        if (!HasHealthReserve())
        {
            return true;
        }
        else if (HasDamageCooldown())
        {
            return immunity.InCooldown();
        }
        else
        {
            return false;
        }
    }

    private bool HasHealthReserve()
    {
        return healthReserve != null;
    }

    private bool HasDamageCooldown()
    {
        return immunity != null;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
