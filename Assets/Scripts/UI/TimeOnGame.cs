using UnityEngine;
using UnityEngine.UI;

public class TimeOnGame : MonoBehaviour
{
    public PlayerStatistics playerStatistics;
    public Text timeSurvived;

    void Update()
    {
        timeSurvived.text = playerStatistics.timeAlive.ToString("F2");
    }
}
