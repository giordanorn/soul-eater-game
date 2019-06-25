using UnityEngine;

/// <summary>Provides functionality for set the direction of the associated object.</summary>
public class DirectionController : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>Event which is invoked when player change its direction. Functions which listen to this event receives one parameter of type Direction</summary>
    public UnityEventDirection OnDirectionChange { private set; get; }

    /// <summary>The object's direction.</summary>
    public Direction direction;

    void Awake()
    {
        OnDirectionChange = new UnityEventDirection();
    }

    /***** API *****/
    /// <summary>Set the direction to look at.</summary>
    public void LookAt(Direction _direction)
    {
        direction = _direction;
        OnDirectionChange.Invoke(direction);
    }

    /// <summary>Set the direction to Up.</summary>
    public void LookUp()
    {
        direction = Direction.Up;
        OnDirectionChange.Invoke(direction);
    }

    /// <summary>Set the direction to Left.</summary>
    public void LookLeft()
    {
        direction = Direction.Left;
        OnDirectionChange.Invoke(direction);
    }

    /// <summary>Set the direction to Right.</summary>
    public void LookRight()
    {
        direction = Direction.Right;
        OnDirectionChange.Invoke(direction);
    }

    /// <summary>Set the direction to Down.</summary>
    public void LookDown()
    {
        direction = Direction.Down;
        OnDirectionChange.Invoke(direction);
    }
}
