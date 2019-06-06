using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BasicMovement : MonoBehaviour
{
    /***** Unity Parameters *****/

    ///<summary>Layer mask used to detect collision with possible obstacles.</summary>
    public LayerMask levelMask;

    /// <summary>Time determined to perform a full movement.</summary>
    public float moveTime = 1f;


    /***** Unity Components *****/

    /// <summary>The rigidbody associated with this entity.</summary>
    /// <value>The rigidbody.</value>
    private Rigidbody2D rb;


    /***** Properties *****/

    /// <summary>The last move performed.</summary>
    /// <value>The last move.</value>
    public Vector3Int LastMove { get; private set; } = Vector3Int.zero;

    /// <summary>Whether this entity or not.</summary>
    /// <value>true if moving, else false.</value>
    public bool IsMoving { get; private set; } = false;

    // Floating-point calculation optimization, not sure if necessary.
    /// <summary>Gets the inverse move time.</summary>
    /// <value>The inverse move time.</value>
    private float InverseMoveTime => 1f / moveTime;


    /**** Private Fields ****/

    /// <summary>The allowed moves for this entity.</summary>
    private readonly Vector3Int[] moves = { Vector3Int.left, Vector3Int.right, Vector3Int.down, Vector3Int.up };

    /***** Unity Methods *****/

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
    }

    void FixedUpdate()
    {
        if (!IsMoving)
        {
            Vector3Int direction = ChooseMove();
            LastMove = direction;

            StartCoroutine(SmoothMovement(Vector3Int.FloorToInt(transform.position + direction)));
        }
    }


    /***** Public methods *****/

    /// <summary>Chooses the move to be performed.</summary>
    /// <returns>The chosen move.</returns>
    public abstract Vector3Int ChooseMove();

    /// <summary>Get all the moves that can be performed by this entity.</summary>
    /// <returns>The valid moves.</returns>
    public Vector3Int[] GetValidMoves()
    {
        return moves.Where(m => CanMove(m)).ToArray();
    }

    /// <summary>Checks whether this entity can move along <paramref name="direction"/>.</summary>
    /// <returns><c>true</c>, if it can move, <c>false</c> otherwise.</returns>
    /// <param name="direction">The direction to move.</param>
    public bool CanMove(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.51f, levelMask);
        return hit.collider == null;
    }


    /***** Private Methods *****/

    /// <summary>
    /// Coroutine to execute a smooth movement from current position to <paramref name="end"/>.
    /// </summary>
    /// <param name="end">The endpoint.</param>
    private IEnumerator SmoothMovement(Vector3 end)
    {
        Vector3 currentPosition = transform.position;

        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (currentPosition - end).sqrMagnitude;

        IsMoving = true;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPosition = Vector3.MoveTowards(rb.position, end, InverseMoveTime * Time.fixedDeltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb.MovePosition(newPosition);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        IsMoving = false;
    }
}
