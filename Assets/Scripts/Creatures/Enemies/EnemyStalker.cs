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
    private GameObject target;

    /// <summary>
    /// Chooses the move to be performed.
    /// </summary>
    /// <returns>The move to be performed.</returns>
    public override Vector3 ChooseMove()
    {
        MapModel map = creature.Map;
        if (map != null && target != null && target.activeSelf)
        {
            Vector3 delta = ModMath.MinDeltaVector(transform.position, target.transform.position, map.MapSize);
            if (delta.sqrMagnitude < stalkRadius * stalkRadius)
            {
                return delta;
            }
        }

        return base.ChooseMove();
    }

    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    void OnDrawGizmos()
    {
        MapModel map = creature.Map;
        if (map != null)
        {
            Vector3 delta = ModMath.MinDeltaVector(transform.position, target.transform.position, map.MapSize);
            Gizmos.DrawLine(transform.position, transform.position + delta);
        }
    }
}
