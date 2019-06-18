using UnityEngine;

[RequireComponent(typeof(Collector))]
public class CountCollectedSouls : MonoBehaviour
{
    public int soulsCollected = 0;

    private void Start()
    {
        
    }

    public void addKill()
    {
        Debug.Log("collected");
        ++soulsCollected;
    }

}