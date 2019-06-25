using UnityEngine;
using UnityEngine.UI;

public class TimeOnGame : MonoBehaviour
{
    private PlayerStatistics playerStatistics;
    public Text timeSurvived;

    void Start ()
    {
        playerStatistics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatistics>();
    }

    void Update()
    {
        timeSurvived.text = playerStatistics.timeAlive.ToString("F2");
    }
}
