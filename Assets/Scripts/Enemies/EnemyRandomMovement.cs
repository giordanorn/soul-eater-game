using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomMovement : BasicMovement
{
    private void Awake()
    {
        moveTime = 0.4f;
    }
    public override Vector3Int choose_move()
    {
        Vector3Int move;
        move = next_move();
        if (move == inverse_last_move)
        {
            return last_move;
        }
        return move;
    }
    public Vector3Int next_move()
    {
        float rand = Random.Range(0, 10);
        Vector3Int[] moves = getValidMoves();
        for (int i = moves.Length - 1; i >= 0; i--)
        {
            if (rand >= (i * 10f) / moves.Length)
            {
                return moves[i];
            }
        }
        return Vector3Int.zero;
    }
}