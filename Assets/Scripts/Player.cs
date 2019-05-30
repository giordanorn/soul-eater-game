using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicMovement
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public override Vector3Int choose_move()
    {
        if (Input.GetKey(right) & can_move(Vector3Int.right))
        {
            return Vector3Int.right;
        }
        if (Input.GetKey(left) & can_move(Vector3Int.left))
        {
            return Vector3Int.left;
        }
        if (Input.GetKey(up) & can_move(Vector3Int.up))
        {
            return Vector3Int.up;
        }
        if (Input.GetKey(down) & can_move(Vector3Int.down))
        {
            return Vector3Int.down;
        }
        return Vector3Int.zero;
    }
}