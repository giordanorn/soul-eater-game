using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(SpriteRenderer))]
public class PlayerLongAttack : MonoBehaviour
{
    public float range = 0.5f;
    public float cost = 10f;
    public float velocity = 1.05f;
    public float attackDuration = 1f;
    public float attackCooldown = 2f;
    public KeyCode attackKey = KeyCode.Space;

    private CircleCollider2D attackCollider;
    private SpriteRenderer attackSprite;
    private Attacker attacker;
    private Creature creature;
    private PlayerDirection playerDirection;
    private Cooldown duration;
    private Cooldown cooldown;
    private bool canAttack = true;
    private Vector3 lastOffset = Vector3Int.zero;
    private Vector3 offset = Vector3Int.zero;

    void Start()
    {
        attackCollider = GetComponent<CircleCollider2D>();
        attackCollider.isTrigger = true;
        attackCollider.enabled = false;

        attackSprite = GetComponent<SpriteRenderer>();
        if (attackSprite != null)
        {
            attackSprite.enabled = false;
        }


        attacker = GetComponentInParent<Attacker>();
        creature = GetComponentInParent<Creature>();

        duration = gameObject.AddComponent<Cooldown>();
        duration.onCooldownEnd.AddListener(StopAttack);

        cooldown = gameObject.AddComponent<Cooldown>();
        duration.onCooldownEnd.AddListener(RecoverAttack);

        playerDirection = GetComponentInParent<PlayerDirection>();
        playerDirection.OnDirectionChange.AddListener(changeAttackDirection);
    }

    void Update()
    {
        PerformAttack();
        AttackMovement();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Creature target = collider.GetComponent<Creature>();
        if (target != null)
        {
            attacker.Hit(target);
            StopAttack();
        }
    }

    private void changeAttackDirection(Direction direction)
    {
        lastOffset = GetOffset(direction);
    }

    private void PerformAttack()
    {
        if (Input.GetKeyDown(attackKey) && canAttack)
        {
            attackCollider.enabled = true;
            if (attackSprite != null)
            {
                attackSprite.enabled = true;
            }
            offset = lastOffset;
            creature.TakeDamage(cost);
            duration.StartCooldown(attackDuration);
            canAttack = false;
        }
    }

    private Vector3 GetOffset(Direction direction)
    {
        if (direction == Direction.Down)
            return new Vector3(0, -range, 0);
        else if (direction == Direction.Left)
            return new Vector3(-range, 0, 0);
        else if (direction == Direction.Up)
            return new Vector3(0, range, 0);
        else if (direction == Direction.Right)
            return new Vector3(range, 0, 0);
        else
            return Vector3Int.zero;
    }

    public void StopAttack()
    {
        attackCollider.enabled = false;
        if (attackSprite != null)
        {
            attackSprite.enabled = false;
        }
        transform.position = attacker.transform.position;
    }

    public void RecoverAttack()
    {
        canAttack = true;
    }

    public void AttackMovement()
    {
        if (attackCollider.enabled)
        {
            offset *= velocity;
            transform.position += offset;
        }
    }
}
