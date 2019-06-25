using UnityEngine;

[RequireComponent(typeof(Creature))]
public class CountTimeAlive : MonoBehaviour
{
    private Creature creature;
    public double timeAlive = 0;

    private void Start()
    {
        creature = GetComponent<Creature>();
    }

    private void LateUpdate()
    {
        if (creature.IsAlive())
        {
            timeAlive += Time.deltaTime;
        }
    }
}