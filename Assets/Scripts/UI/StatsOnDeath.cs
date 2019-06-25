using UnityEngine;
using UnityEngine.UI;

public class StatsOnDeath : MonoBehaviour
{
    private PlayerStatistics playerStatistics;
    private Creature player;

    public Text timeSurvived;
    public Text enemies;
    public Text souls;
    public GameObject panel;

    void Start ()
    {
        panel.SetActive(false);

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        playerStatistics = go.GetComponent<PlayerStatistics>();
        player = go.GetComponent<Creature>();

        player.addActionOnDeath(showStats);

    }
    void showStats ()
    {
        panel.SetActive(true);
        timeSurvived.text = playerStatistics.timeAlive.ToString("F2");
        enemies.text = playerStatistics.kills.ToString();
        souls.text = playerStatistics.soulsCollected.ToString();
    }
}
