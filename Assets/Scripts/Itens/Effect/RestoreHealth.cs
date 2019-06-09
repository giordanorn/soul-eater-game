using UnityEngine;

/// <summary>
/// An Effect to restore a determined quantity of Health on a Creature.
/// </summary>
public class RestoreHealth : Effect
{
    /// <summary>
    /// The value of Health to be restored.
    /// </summary>
    public float value = 10;
    public override void ApplyEffect(Collector collector)
    {
        if (collector.IsCreature())
        {
            HealthReserve healthReserve = collector.GetComponent<HealthReserve>();
            if (healthReserve != null)
            {
                healthReserve.Increase(value);
            }
            else
            {
                Debug.LogWarning("Cannot restore health. HealthReserve missing!");
            }
        }
        else
        {
            Debug.LogWarning("Cannot restore health. Not a Creature!");
        }
    }
}
