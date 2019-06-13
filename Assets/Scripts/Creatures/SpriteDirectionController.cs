using UnityEngine;

/// <summary>Provides functionality for changing sprite based on the associated direction.</summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDirectionController : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>Sprite looking down.</summary>
    public Sprite downSprite;

    /// <summary>Sprite looking left.</summary>
    public Sprite leftSprite;

    /// <summary>Sprite looking up.</summary>
    public Sprite upSprite;

    /// <summary>Sprite looking right.</summary>
    public Sprite rightSprite;


    /***** Internal *****/

    /// <summary>The object's sprite renderer.</summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>The object's direction.</summary>
    private Directions direction;


    /***** API *****/

    /// <summary>The possible directions for an object.</summary>
    public enum Directions
    {
        Down,
        Left,
        Up,
        Right
    }

    /// <summary>Manipulates the object's direction.</summary>
    public Directions Direction
    {
        /// <summary>Gets the direction.</summary>
        /// <returns>The direction.</returns>
        get { return direction; }

        /// <summary>Sets the direction, changing sprite accordingly.</summary>
        /// <param name="value">Value.</param>
        set
        { 
            switch (value)
            {
                case Directions.Down:
                    spriteRenderer.sprite = downSprite;
                    break;

                case Directions.Left:
                    spriteRenderer.sprite = leftSprite;
                    break;

                case Directions.Up:
                    spriteRenderer.sprite = upSprite;
                    break;

                case Directions.Right:
                    spriteRenderer.sprite = rightSprite;
                    break;
            }

            direction = value;
        }
    }


    /***** Unity Methods *****/

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
