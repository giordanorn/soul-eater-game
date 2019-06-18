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

    /// <summary>Event which is invoked when player change its direction. Functions which listen to this event receives one parameter of type Direction</summary>
    public UnityEventDirection OnDirectionChange { private set; get; }

    /***** Internal *****/

    private DirectionController directionController;


    /***** Unity Methods *****/

    void Awake()
    {
        OnDirectionChange = new UnityEventDirection();
    }

    void Start()
    {
        directionController = GetComponent<DirectionController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(downKey))
        {
            directionController.LookDown();
            OnDirectionChange.Invoke(Direction.Down);
        }

        if (Input.GetKeyDown(leftKey))
        {
            directionController.LookLeft();
            OnDirectionChange.Invoke(Direction.Left);
        }

        if (Input.GetKeyDown(upKey))
        {
            directionController.LookUp();
            OnDirectionChange.Invoke(Direction.Up);
        }

        if (Input.GetKeyDown(rightKey))
        {
            directionController.LookRight();
            OnDirectionChange.Invoke(Direction.Right);
        }
    }
}
