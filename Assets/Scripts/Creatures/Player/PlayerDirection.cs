using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Controls Player direction based on input.</summary>
[RequireComponent(typeof(SpriteDirectionController))]
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

    private SpriteDirectionController directionalSprite;


    /***** Unity Methods *****/

    void Start()
    {
        directionalSprite = GetComponent<SpriteDirectionController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(downKey))
        {
            directionalSprite.LookDown();
        }

        if (Input.GetKeyDown(leftKey))
        {
            directionalSprite.LookLeft();
        }

        if (Input.GetKeyDown(upKey))
        {
            directionalSprite.LookUp();
        }

        if (Input.GetKeyDown(rightKey))
        {
            directionalSprite.LookRight();
        }
    }
}
