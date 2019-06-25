using System.Linq;
using UnityEngine;

public class EnemyRandomGridMovement : EnemyMovement
{
    /// <summary>The allowed moves for this entity.</summary>
    private readonly Vector3Int[] _moves = { Vector3Int.left, Vector3Int.right, Vector3Int.down, Vector3Int.up };

    /// <summary>Get all the moves that can be performed by this entity.</summary>
    /// <returns>The valid moves.</returns>
    private Vector3Int[] GetValidMoves()
    {
        return _moves.Where(m => CanMove(m)).ToArray();
    }

    /// <summary>Selects a random move from the available moves.</summary>
    /// <returns>The selected move.</returns>
    public override Vector3 ChooseMove()
    {
        Vector3Int[] moves = GetValidMoves();
        int randIndex = Random.Range(0, moves.Length);

        if (randIndex == moves.Length)
            return Vector3Int.zero;

        return moves[randIndex];
    }
}