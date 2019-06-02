using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BasicSoul : MonoBehaviour
{
    public int souls;
    public int maxSouls;
    protected bool healing;
    protected int healTax;
    protected float healInterval;
    protected Collider2D coll2d;
    private void Awake()
    {
        healing = false;
        healTax = 1;
        coll2d = GetComponent<Collider2D>();
    }

    protected IEnumerator HealSoul(float time)
    {
        healing = true;
        souls += healTax;
        if (souls > maxSouls)
        {
            souls = maxSouls;
        }
        yield return new WaitForSeconds(time);
        healing = false;
    }

    public void hit()
    {
        --souls;
        if (souls <= 0)
        {
            SelfDestroy();
        }
    }
    public void SelfDestroy()
    {
        LastBreath();
        Destroy(gameObject);
    }
    public virtual void LastBreath()
    {

    }
}