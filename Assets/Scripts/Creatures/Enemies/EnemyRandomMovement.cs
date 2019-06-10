using UnityEngine;

public class EnemyRandomMovement : EnemyGridMovement
{
    /// <summary>Selects a random move from the available moves.</summary>
    /// <returns>The selected move.</returns>
    public override Vector3Int ChooseMove()
    {
        Vector3Int[] moves = GetValidMoves();
        int randIndex = Random.Range(0, moves.Length);

        if (randIndex == moves.Length)
            return Vector3Int.zero;

        return moves[randIndex];
    }
}