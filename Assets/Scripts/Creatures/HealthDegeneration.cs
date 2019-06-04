using System;
using UnityEngine;

[RequireComponent(typeof(HealthReserve))]
public class HealthDegeneration : MonoBehaviour
{
    public float degeneratePerSecond;
    private HealthReserve healthReserve;

    void Start()
    {
        healthReserve = GetComponent<HealthReserve>();
        
        if (degeneratePerSecond < 0.0f)
        {
            degeneratePerSecond = 0.0f;
            Debug.LogWarning("You shoudn't set a negative value to the variable \"Degenerate Per Second\", if you mind to, use HealthRegeneration instead. The value was set to zero.");
        }
    }

    void Update()
    {
        healthReserve.Decrease(Time.deltaTime * degeneratePerSecond);
    }
}
