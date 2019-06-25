using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>Controls Player direction based on input.</summary>
[RequireComponent(typeof(DirectionController))]
public class PlayerDirection : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>Key for looking down.</summary>
    public KeyCode downKey = KeyCode.DownArrow;

    /// <summary>Key for looking left.</summary>
    public KeyCode leftKey = KeyCode.LeftArrow;

    /// <summary>Key for looking up.</summary>
    public KeyCode upKey = KeyCode.UpArrow;

    /// <summary>Key for looking right.</summary>
    public KeyCode rightKey = KeyCode.RightArrow;

    /***** Internal *****/

    private DirectionController directionController;


    /***** Unity Methods *****/



    void Start()
    {
        directionController = GetComponent<DirectionController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(downKey))
        {
            directionController.LookDown();
        }

        if (Input.GetKeyDown(leftKey))
        {
            directionController.LookLeft();
        }

        if (Input.GetKeyDown(upKey))
        {
            directionController.LookUp();
        }

        if (Input.GetKeyDown(rightKey))
        {
            directionController.LookRight();
        }
    }
}
