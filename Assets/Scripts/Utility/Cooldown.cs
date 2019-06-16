using UnityEngine;

public class Cooldown : MonoBehaviour
{
    private float currentCooldown;

    /***** API *****/

    public bool InCooldown()
    {
        return currentCooldown != 0.0f;
    }

    public void StartCooldown(float cooldownTime)
    {
        currentCooldown = cooldownTime;
    }

    /***** Internal *****/

    private void DiscountCooldown()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0.0f)
            currentCooldown = 0.0f;
    }

    /***** Unity Methods *****/

    void Start()
    {
        currentCooldown = 0.0f;
    }

    void Update()
    {
        if (InCooldown())
        {
            DiscountCooldown();
        }
    }
}
