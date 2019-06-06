using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public float speed = 0.2f;
    void FixedUpdate()
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