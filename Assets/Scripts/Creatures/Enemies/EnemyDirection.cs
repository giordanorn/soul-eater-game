using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DirectionController))]
public class EnemyDirection : MonoBehaviour
{
    

    /***** Internal *****/

    private DirectionController directionController;
    private EnemyRandomGridMovement enemyRandomMovement;


    /***** Unity Methods *****/



    void Start()
    {
        enemyRandomMovement = GetComponent<EnemyRandomGridMovement>();
        directionController = GetComponent<DirectionController>();
    }

    void Update()
    {
        if (enemyRandomMovement.LastMove == Vector3Int.down)
        {
            directionController.LookDown();
        }

        if (enemyRandomMovement.LastMove == Vector3Int.left)
        {
            directionController.LookLeft();
        }

        if (enemyRandomMovement.LastMove == Vector3Int.up)
        {
            directionController.LookUp();
        }

        if (enemyRandomMovement.LastMove == Vector3Int.right)
        {
            directionController.LookRight();
        }
    }
}
