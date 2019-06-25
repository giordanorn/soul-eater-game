using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Creature))]
public abstract class EnemyMovement : MonoBehaviour
{
    /***** Unity Parameters *****/

    /// <summary>Time determined to perform a full movement.</summary>
    public float moveTime = 1f;


    /***** Unity Methods *****/

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;

        creature = GetComponent<Creature>();
    }


    private float counter = 0f;
    private Vector3 direction;

    void FixedUpdate()
    {
        if (!IsMoving)
        {
            direction = ChooseMove();
            LastMove = direction;

            if (direction.magnitude > float.Epsilon)
                IsMoving = true;
        }
        else
        {
            if (CanMove(direction))
            {
                transform.position += direction * Time.fixedDeltaTime;
            }

            counter += Time.fixedDeltaTime;
            if (counter >= 1f)
            {
                IsMoving = false;
                counter = 0f;
            }
        }
    }


    /***** API *****/

    /// <summary>The last move performed.</summary>
    /// <value>The last move.</value>
    public Vector3 LastMove { get; private set; } = Vector3.zero;

    /// <summary>Whether this entity or not.</summary>
    /// <value>true if moving, else false.</value>
    public bool IsMoving { get; private set; } = false;

    /// <summary>Chooses the move to be performed.</summary>
    /// <returns>The chosen move.</returns>
    public abstract Vector3 ChooseMove();

    /// <summary>Checks whether this entity can move along <paramref name="direction"/>.</summary>
    /// <returns><c>true</c>, if it can move, <c>false</c> otherwise.</returns>
    /// <param name="direction">The direction to move.</param>
    public bool CanMove(Vector3 direction)
    {
        if (creature.Map == null)
            return false;

        Vector3 end = transform.position + direction;
        return creature.Map.SameRegion(end, transform.position);
    }


    /**** Internal ****/

    private Vector3 delta;

    // Floating-point calculation optimization, not sure if necessary.
    /// <summary>Gets the inverse move time.</summary>
    /// <value>The inverse move time.</value>
    private float InverseMoveTime => 1f / moveTime;

    /// <summary>The rigidbody associated with this entity.</summary>
    /// <value>The rigidbody.</value>
    private Rigidbody2D rb;

    /// <summary>The creature component.</summary>
    protected Creature creature;

    /*
    /// <summary>
    /// Coroutine to execute a smooth movement from current position to <paramref name="end"/>.
    /// </summary>
    /// <param name="end">The endpoint.</param>
    private IEnumerator SmoothMovement(Vector3 end)
    {
        Vector3 currentPosition = transform.position;
        Vector2 delta = ModMath.MinDeltaVector(currentPosition, end, creature.Map.MapSize);

        //Calculate the remaining distance to move based on the square magnitude of the delta vector from current position to end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = delta.sqrMagnitude;

        IsMoving = true;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPosition = Vector3.MoveTowards(rb.position, rb.position + delta, InverseMoveTime * Time.fixedDeltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb.MovePosition(newPosition);

            delta = ModMath.MinDeltaVector(transform.position, end, creature.Map.MapSize);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = delta.sqrMagnitude;

            this.delta = delta;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        IsMoving = false;
    }
    */

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + delta);
    }
}
