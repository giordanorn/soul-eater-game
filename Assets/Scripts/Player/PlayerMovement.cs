using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PlayerMovement : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>The Key that move the GameObject upwards.</summary>
    public KeyCode up = KeyCode.W;
    /// <summary>The Key that move the GameObject downward.</summary>
    public KeyCode down = KeyCode.S;
    /// <summary>The Key that move the GameObject to left.</summary>
    public KeyCode left = KeyCode.A;
    /// <summary>The Key that move the GameObject to right.</summary>
    public KeyCode right = KeyCode.D;
    /// <summary>The speed that the GameObject moves.</summary>
    public float speed = 3f;

    /***** Unity Methods *****/
    void FixedUpdate()
    {
        Move();
    }

    /// <summary>Get the Keys pressed and move the GameObject</summary>
    void Move()
    {
        float Hmove = 0.0f;
        float Vmove = 0.0f;
        if (Input.GetKey(right))
        {
            Hmove += 1;
        }
        if (Input.GetKey(left))
        {
            Hmove += -1;
        }
        if (Input.GetKey(up))
        {
            Vmove += 1;
        }
        if (Input.GetKey(down))
        {
            Vmove += -1;
        }
        transform.position += new Vector3(Hmove * speed, Vmove * speed, 0) * Time.deltaTime;
    }
}