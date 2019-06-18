using UnityEngine;
using UnityEngine.Events;
public class Creature : MonoBehaviour
{
    private UnityEvent beforeDie;

    private HealthReserve healthReserve;

    private ImmunityAfterTakeDamage immunity;

    private void Awake()
    {
        if (beforeDie == null)
        {
            beforeDie = new UnityEvent();
        }
    }

    void Start()
    {


        healthReserve = GetComponent<HealthReserve>();
        immunity = GetComponent<ImmunityAfterTakeDamage>();
    }

    void Update()
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

    public bool IsAlive()
    {
        return HasHealthReserve() && !healthReserve.IsEmpty();
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

    public bool HasHealthReserve()
    {
        return healthReserve != null;
    }

    public bool HasDamageCooldown()
    {
        return immunity != null;
    }

    public void addActionOnDeath(UnityAction function)
    {
        beforeDie.AddListener(function);
    }

    private void Die()
    {
        beforeDie.Invoke();
        gameObject.SetActive(false);
    }
}
