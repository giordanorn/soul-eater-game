using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicCloseAttack : MonoBehaviour
{

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public int damage = 1;
    public BasicCloseAttack attackPrefab;

    private void Awake() {
        up = KeyCode.UpArrow;
        down = KeyCode.DownArrow;
        left = KeyCode.LeftArrow;
        right = KeyCode.RightArrow;
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    public void attackDirection(Vector3Int direction)
    {
        BasicCloseAttack ex = Instantiate(attackPrefab, transform.position + direction, Quaternion.identity);
        ex.damage = damage;
    }

    public void checkAttack()
    {
        if (Input.GetKey(right))
        {
            attackDirection(Vector3Int.right);
            return;
        }
        if (Input.GetKey(left))
        {
            attackDirection(Vector3Int.left);
            return;
        }
        if (Input.GetKey(up))
        {
            attackDirection(Vector3Int.up);
            return;
        }
        if (Input.GetKey(down))
        {
            attackDirection(Vector3Int.down);
            return;
        }
    }
}