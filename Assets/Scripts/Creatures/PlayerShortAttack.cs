using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerShortAttack : MonoBehaviour
{
    public float range = 1;
    public float attackDuration = 0.1f;
    //public float attackCooldown = 0.5f;

    private CircleCollider2D attackCollider;

    private Attacker attacker;
    private DirectionController playerDirection;

    private Cooldown duration;
    //private Cooldown  cooldown;

    void Start()
    {
        attackCollider = GetComponent<CircleCollider2D>();
        attackCollider.isTrigger = true;
        attackCollider.enabled = false;

        attacker = GetComponentInParent<Attacker>();

        duration = gameObject.AddComponent<Cooldown>();
        duration.onCooldownEnd.AddListener(StopAttack);

        playerDirection = GetComponentInParent<DirectionController>();
        playerDirection.OnDirectionChange.AddListener(PerformAttack);
        // cooldown = gameObject.AddComponent<Cooldown>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Creature target = collider.GetComponent<Creature>();
        if (target != null)
        {
            attacker.Hit(target);
        }
    }

    private void PerformAttack(Direction direction)
    {
        attackCollider.offset = GetOffset(direction);
        attackCollider.enabled = true;
        duration.StartCooldown(attackDuration);
    }

    private Vector2 GetOffset(Direction direction)
    {
        if (direction == Direction.Down)
            return new Vector2(0, -range);
        else if (direction == Direction.Left)
            return new Vector2(-range, 0);
        else if (direction == Direction.Up)
            return new Vector2(0, range);
        else if (direction == Direction.Right)
            return new Vector2(range, 0);
        else
            return Vector2Int.zero;
    }

    public void StopAttack()
    {
        attackCollider.enabled = false;
        attackCollider.offset = Vector2Int.zero;
    }
}
