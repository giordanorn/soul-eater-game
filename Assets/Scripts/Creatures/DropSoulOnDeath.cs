using UnityEngine;


[RequireComponent(typeof(Creature))]
public class DropSoulOnDeath : MonoBehaviour
{
    public int value = 10;
    public Collectable soulPrefab;
    private Creature creature;

    void Start()
    {
        creature = GetComponent<Creature>();
        creature.addActionOnDeath(createSoul);
    }

    private void createSoul()
    {
        Collectable soul = Instantiate(soulPrefab, transform.position, Quaternion.identity) as Collectable;
        Heal heal = soul.GetComponent<Heal>();
        heal.value = value;
    }
}
