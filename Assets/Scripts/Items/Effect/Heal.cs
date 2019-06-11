using UnityEngine;

/// <summary>
/// An Effect to restore a determined quantity of Health on a Creature.
/// </summary>
public class Heal : Effect
{
    /// <summary>
    /// The amount of Health to be restored.
    /// </summary>
    public float amount = 10.0f;
    public override void ApplyEffect(Collector collector)
    {
        if (collector.IsCreature())
        {
            Creature creature = collector.GetComponent<Creature>();
            if (creature.HasHealthReserve())
            {
                HealthReserve healthReserve = collector.GetComponent<HealthReserve>();
                healthReserve.Increase(amount);
            }
            else
            {
                Debug.LogWarning("Can't heal. HealthReserve is missing!");
            }
        }
        else
        {
            Debug.LogWarning("Can't heal. Not a Creature!");
        }
    }
}
