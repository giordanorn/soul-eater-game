using System;
using UnityEngine;

// Pode ser que nem precisemos restringir, pois ainda não estamos usando o fato de que ele é um Creature pra nada.
[RequireComponent(typeof(Creature))]
public class HealthReserve : MonoBehaviour
{
    public float maximumHealth;
    public float CurrentHealth {get; private set;}

    void Start()
    {
        CurrentHealth = maximumHealth;
    }

    public void Increase(float healthToIncrease)
    {
        CurrentHealth = Math.Min(CurrentHealth + healthToIncrease, maximumHealth);
    }
    
    public void Decrease(float healthToDecrease)
    {
        CurrentHealth = Math.Max(CurrentHealth - healthToDecrease, 0.0f);
    }

    public bool IsEmpty()
    {
        return CurrentHealth == 0.0f;
    }
}
