using UnityEngine;

public class RestoreHealth : Effect
{
    public float value = 10;
    public override void Do(Collector collector)
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
