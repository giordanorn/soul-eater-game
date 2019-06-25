using UnityEngine;

public class EnemyStalker : EnemyRandomGridMovement
{
    /// <summary>
    /// The radius of the disk in which the target will be stalked.
    /// </summary>
    public float stalkRadius;

    /// <summary>
    /// The target to be stalked.
    /// </summary>
    public GameObject target;

    /// <summary>
    /// Chooses the move to be performed.
    /// </summary>
    /// <returns>The move to be performed.</returns>
    public override Vector3Int ChooseMove()
    {
        Vector3 delta = target.transform.position - transform.position;
        if (delta.sqrMagnitude < stalkRadius * stalkRadius)
        {
            return Vector3Int.FloorToInt(delta.normalized);
        }

        return base.ChooseMove();
    }
}
