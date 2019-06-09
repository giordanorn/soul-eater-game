using UnityEngine;

/// <summary>
/// Simple interface to indicate that is a effect.
/// </summary>
public abstract class Effect: MonoBehaviour
{
    /// <summary>
    /// Method to apply an effect on a Collector.
    /// </summary>
    /// <param name="collector">The Collector target of the effect.</param>
    public abstract void Do(Collector collector);
}
