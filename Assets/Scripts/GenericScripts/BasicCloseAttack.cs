using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCloseAttack : MonoBehaviour
{
    public int damage = 1;
    void Start()
    {
        StartCoroutine(destroy());
    }
    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Debug.Log("hit: " + damage.ToString());
            //col.GetComponent<GenericEnemy>().hit();
        }
    }
}