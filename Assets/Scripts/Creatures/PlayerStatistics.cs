using UnityEngine;

[RequireComponent(typeof(Collector), typeof(Attacker), typeof(Creature))]
public class PlayerStatistics : MonoBehaviour
{
    private Creature creature;
    public int soulsCollected = 0;
    public double timeAlive = 0;
    public int kills = 0;

    private void Start()
    {
        GetComponent<Collector>().addActionOnSoulCollected(addSoul);
        GetComponent<Attacker>().addActionOnKill(addKill);
        creature = GetComponent<Creature>();
    }

    public void addSoul()
    {
        ++soulsCollected;
    }

    public void addKill()
    {
        ++kills;
    }

    private void LateUpdate()
    {
        if (creature.IsAlive())
        {
            timeAlive += Time.deltaTime;
        }
    }
}