using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class CountKills : MonoBehaviour
{
    public int kills = 0;

    private void Start()
    {
        GetComponent<Attacker>().addActionOnKill(addKill);
    }

    public void addKill()
    {
        Debug.Log("kill!");
        ++kills;
    }

}