using UnityEngine;

/// <summary>Provides functionality for set the direction of the associated object.</summary>
public class DirectionController : MonoBehaviour
{
    /// <summary>The object's direction.</summary>
    public Direction direction;

    /***** API *****/
    /// <summary>Set the direction to look at.</summary>
    public void LookAt(Direction _direction)
    {
        direction = _direction;
    }

    /// <summary>Set the direction to Up.</summary>
    public void LookUp()
    {
        direction = Direction.Up;
    }
    
    /// <summary>Set the direction to Left.</summary>
    public void LookLeft()
    {
        direction = Direction.Left;
    }
    
    /// <summary>Set the direction to Right.</summary>
    public void LookRight()
    {
        direction = Direction.Right;
    }
    
    /// <summary>Set the direction to Down.</summary>
    public void LookDown()
    {
        direction = Direction.Down;
    }
}
