using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicMovement : MonoBehaviour
{
    public LayerMask levelMask;

    protected Rigidbody2D rb2d;

    public bool isMoving = false;
    public float moveTime = 1f;
    public float inverseMoveTime;
    public Vector3Int last_move;
    protected Vector3Int[] moves = { Vector3Int.left, Vector3Int.right, Vector3Int.down, Vector3Int.up };
    protected int[] index = new int[4];

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0.0f;
        inverseMoveTime = 1f / moveTime;
    }

    void FixedUpdate()
    {
        if (!isMoving)
        {
            Vector3Int direction = choose_move();
            last_move = direction;
            StartCoroutine(SmoothMovement((transform.position + direction).Floor()));
        }
    }

    public abstract Vector3Int choose_move();

    // Taken from Unity Tutorials
    protected IEnumerator SmoothMovement(Vector3 end, Vector3 temp_pos = default(Vector3))
    {
        if (end == null)
        {
            Debug.Log(end);
            Debug.Log(temp_pos);
        }
        if (temp_pos == default(Vector3))
        {
            temp_pos = transform.position;
        }
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (temp_pos - end).sqrMagnitude;
        isMoving = true;
        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPosition = Vector3.MoveTowards(rb2d.position, end, inverseMoveTime * Time.fixedDeltaTime);
            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2d.MovePosition(newPosition);
            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
        isMoving = false;
    }

    public Vector3Int[] getValidMoves()
    {
        int size = 0;
        for (int i = 0; i < moves.Length; i++)
        {
            if (can_move(moves[i]))
            {
                index[size] = i;
                ++size;
            }
        }
        Vector3Int[] valid = new Vector3Int[size];
        for (int i = 0; i < size; i++)
        {
            valid[i] = moves[index[i]];
        }
        return valid;
    }

    public bool can_move(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.51f, levelMask);
        if (hit.collider == null)
        {
            return true;
        }
        return false;
    }
}
