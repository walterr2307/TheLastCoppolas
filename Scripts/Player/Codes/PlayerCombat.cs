using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public PlayerMovement movement;
    public Transform attackPoint;
    public LayerMask enemyMask;

    protected bool flipImage;
    protected int attackNumber = 1;
    protected int damage = 20;
    protected int knockBackForce = 30;
    protected float attackRadius = 1.5f;
    protected float attackTimer = 0f;
    protected float attackTime = 0.5f;
    protected float faceDirection = 1f;

    protected InputManager controls;
    protected Collider2D enemy;

    protected void Start()
    {
        controls = InputManager.Instantiate();
    }

    protected void FixedUpdate()
    {
        if (attackTimer <= 0f && controls.IsAttacking())
            Attack();
        else
            attackTimer -= Time.deltaTime;
    }

    protected virtual void Attack()
    {
        enemy = GetClosestEnemy();

        float x = transform.localScale.x;
        float y = 0f;

        if (enemy != null)
        {
            x = enemy.transform.position.x - rb.position.x;
            y = enemy.transform.position.y - rb.position.y;
        }

        attackNumber = (attackNumber == 1) ? 2 : 1;
        rb.linearVelocity = Vector2.zero;
        attackTimer = attackTime;
        faceDirection = transform.localScale.x;
        flipImage = false;
        anim.speed = 1f;

        if (Mathf.Abs(x) >= Mathf.Abs(y))
        {
            if ((faceDirection > 0f && x < 0f) || (faceDirection < 0f && x > 0f))
            {
                flipImage = true;
                Flip();
            }
            anim.Play("Attack Side " + attackNumber);
        }
        else if (y > 0)
        {
            anim.Play("Attack Up " + attackNumber);
        }
        else
        {
            anim.Play("Attack Down " + attackNumber);
        }

        movement.LockedMovement();
    }

    protected Collider2D GetClosestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyMask);
        return enemies.Length > 0 ? enemies[0] : null;
    }

    public void CauseDamage()
    {
        if (enemy != null)
        {
            if (Vector2.Distance(enemy.transform.position, rb.position) <= attackRadius)
            {
                enemy.GetComponent<EnemyMovement>()?.KnockBack(knockBackForce, rb.position.x, rb.position.y);
                enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            }

            enemy = null;
        }
    }

    protected void Flip()
    {
        if (flipImage)
        {
            faceDirection *= -1;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * faceDirection;
            transform.localScale = scale;
        }
    }
}
