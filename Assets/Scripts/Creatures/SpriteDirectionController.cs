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

    /***** Unity Methods *****/

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
