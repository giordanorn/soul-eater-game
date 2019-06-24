using UnityEngine;
using UnityEngine.UI;

public class StatsOnDeath : MonoBehaviour
{
    public PlayerStatistics playerStatistics;
    public Text timeSurvived;
    public Text enemies;
    public Text souls;
    public GameObject panel;
    public Creature player;

    void Start()
    {
        panel.SetActive(false);
        player.addActionOnDeath(showStats);
    }
    void showStats(){
        panel.SetActive(true);
        timeSurvived.text = playerStatistics.timeAlive.ToString("F2");
        enemies.text = playerStatistics.kills.ToString();
        souls.text = playerStatistics.soulsCollected.ToString();
    }
}
