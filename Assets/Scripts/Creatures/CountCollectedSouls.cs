using UnityEngine;

[RequireComponent(typeof(Collector))]
public class CountCollectedSouls : MonoBehaviour
{
    public int soulsCollected = 0;

    private void Start()
    {
        GetComponent<Collector>().addActionOnSoulCollected(addSoul);
    }

    public void addSoul()
    {
        ++soulsCollected;
    }

}