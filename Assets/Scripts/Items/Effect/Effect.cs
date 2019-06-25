using UnityEngine;

/// <summary>
/// Simple interface to indicate that is a effect.
/// </summary>
public abstract class Effect : MonoBehaviour
{
    /// <summary>
    /// Method to apply an effect on a GameObject.
    /// </summary>
    /// <param name="collector">The GameObject target of the effect.</param>
    public abstract void ApplyEffect(GameObject gameObject);

    public Component[] aplicableTo;

}
