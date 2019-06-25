using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyShortAttack : MonoBehaviour
{
    public float range = 1;
    public float attackDuration = 0.1f;
    public float attackCooldown = 1f;

    private CircleCollider2D attackCollider;

    private Attacker attacker;
    private DirectionController enemyDirection;

    private Cooldown duration;
    private Cooldown cooldown;

    private bool canAttack = true;
    void Start()
    {
        attackCollider = GetComponent<CircleCollider2D>();
        attackCollider.isTrigger = true;
        attackCollider.enabled = false;

        attacker = GetComponentInParent<Attacker>();

        duration = gameObject.AddComponent<Cooldown>();
        duration.onCooldownEnd.AddListener(StopAttack);

        cooldown = gameObject.AddComponent<Cooldown>();
        cooldown.onCooldownEnd.AddListener(RecoverAttack);

        enemyDirection = GetComponentInParent<DirectionController>();
        enemyDirection.OnDirectionChange.AddListener(PerformAttack);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Creature target = collider.GetComponent<Creature>();
        if (target != null && target.CompareTag("Player"))
        {
            attacker.Hit(target);
        }
    }

    private void PerformAttack(Direction direction)
    {
        if (canAttack)
        {
            attackCollider.offset = GetOffset(direction);
            attackCollider.enabled = true;
            duration.StartCooldown(attackDuration);
            cooldown.StartCooldown(attackCooldown);
            canAttack = false;
        }

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
    public void RecoverAttack()
    {
        canAttack = true;
    }
}
