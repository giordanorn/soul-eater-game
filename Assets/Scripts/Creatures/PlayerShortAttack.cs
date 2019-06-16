using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerShortAttack : MonoBehaviour
{
    public float range = 1;
    public float attackDuration = 0.1f;
    public float attackCooldown = 0.5f;
    public KeyCode hitKey = KeyCode.Space;

    private CircleCollider2D attackCollider;

    private Attacker attacker;

    private Vector2 downOffset;
    private Vector2 leftOffset;
    private Vector2 upOffset;
    private Vector2 rightOffset;

    private Cooldown duration;
    private Cooldown cooldown;

    void Start()
    {
        attackCollider = GetComponent<CircleCollider2D>();
        attackCollider.isTrigger = true;
        
        attacker = GetComponentInParent<Attacker>();

        downOffset = new Vector2(0, -range);
        leftOffset = new Vector2(-range, 0);
        upOffset = new Vector2(0, range);
        rightOffset = new Vector2(range, 0);

        duration = gameObject.AddComponent<Cooldown>();
        cooldown = gameObject.AddComponent<Cooldown>();
    }

    void Update()
    {
        UpdateAttackCollider();
        if (Input.GetKeyDown(hitKey) && !cooldown.InCooldown())
        {
            PerformShortAttack();
            cooldown.StartCooldown(attackCooldown);
        }
        if (!duration.InCooldown())
        {
            attackCollider.enabled = false;
        }
    }

    private void UpdateAttackCollider()
    {
        switch (GetComponentInParent<SpriteDirectionController>().Direction)
        {
            case SpriteDirectionController.Directions.Down:
                attackCollider.offset = downOffset;
                break;
            case SpriteDirectionController.Directions.Left:
                attackCollider.offset = leftOffset;
                break;
            case SpriteDirectionController.Directions.Up:
                attackCollider.offset = upOffset;
                break;
            case SpriteDirectionController.Directions.Right:
                attackCollider.offset = rightOffset;
                break;
            default:
                break;
        }
    }

    private void PerformShortAttack()
    {
        attackCollider.enabled = true;
        duration.StartCooldown(attackDuration);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Creature target = collider.GetComponent<Creature>();
        if (target != null)
        {
            attacker.Hit(target);
        }
    }
}
