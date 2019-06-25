using UnityEngine;
using UnityEngine.UI;

public class TimeOnGame : MonoBehaviour
{
    private PlayerStatistics playerStatistics;
    public Text timeSurvived;

    void Awake ()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
        {
            Debug.Log("lixo 1");
        }
        playerStatistics = player.GetComponent<PlayerStatistics>();
        if (!playerStatistics)
        {
            Debug.Log("lixo 2");
        }
    }

    void Update()
    {
        timeSurvived.text = playerStatistics.timeAlive.ToString("F2");
    }
}
