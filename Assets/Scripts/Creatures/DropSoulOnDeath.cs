using UnityEngine;


[RequireComponent(typeof(Creature))]
public class DropSoulOnDeath : MonoBehaviour
{
    public int healAmount = 10;
    public Collectable soulPrefab;
    private Creature creature;

    void Start()
    {
        creature = GetComponent<Creature>();
        creature.addActionOnDeath(dropSoul);
    }

    private void dropSoul()
    {
        Collectable soul = Instantiate(soulPrefab, transform.position, Quaternion.identity) as Collectable;
        Heal heal = soul.GetComponent<Heal>();
        heal.amount = healAmount;
    }
}
