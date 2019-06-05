using System;
using UnityEngine;

[RequireComponent(typeof(HealthReserve))]
public class HealthRegeneration : MonoBehaviour
{
    public float regeneratePerSecond;
    private HealthReserve healthReserve;

    void Start()
    {
        healthReserve = GetComponent<HealthReserve>();

        if (regeneratePerSecond < 0.0f)
        {
            regeneratePerSecond = 0.0f;
            Debug.LogWarning("You shoudn't set a negative value to the variable \"Regenerate Per Second\", if you mind to, use HealthDegeneration instead. The value has been changed to zero.");
        }
    }

    void Update()
    {
        healthReserve.Increase(Time.deltaTime * regeneratePerSecond);
    }
}
