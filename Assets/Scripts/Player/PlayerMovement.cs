using UnityEngine;

public class PlayerMovement : BasicMovement
{
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    /// <summary>Determines the move according to player input.</summary>
    /// <returns>The determined move.</returns>
    public override Vector3Int ChooseMove()
    {
        if (Input.GetKey(right) & CanMove(Vector3Int.right))
        {
            return Vector3Int.right;
        }

        if (Input.GetKey(left) & CanMove(Vector3Int.left))
        {
            return Vector3Int.left;
        }

        if (Input.GetKey(up) & CanMove(Vector3Int.up))
        {
            return Vector3Int.up;
        }

        if (Input.GetKey(down) & CanMove(Vector3Int.down))
        {
            return Vector3Int.down;
        }

        return Vector3Int.zero;
    }
}