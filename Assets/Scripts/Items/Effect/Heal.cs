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
    public override void ApplyEffect(GameObject gameObject)
    {
        Creature creature = gameObject.GetComponent<Creature>();
        if (creature != null)
        {
            HealthReserve healthReserve = creature.GetComponent<HealthReserve>();
            healthReserve.Increase(amount);
        }
        else
        {
            Debug.LogWarning("Can't heal. Not a Creature!");
        }
    }
}
