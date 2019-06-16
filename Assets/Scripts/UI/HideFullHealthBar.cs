using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBar), typeof(Image))]
public class HideFullHealthBar : MonoBehaviour
{

    private HealthBar healthBar;
    private Image image;

    void Start()
    {
        healthBar = GetComponent<HealthBar>();
        image = GetComponent<Image>();

    }
    void Update()
    {
        if (healthBar.healthReserve.CurrentHealth == healthBar.healthReserve.maximumHealth)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
        }
    }
}
