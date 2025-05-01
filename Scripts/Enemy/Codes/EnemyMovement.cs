using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int speed, damage;
    public float visionRadius, attackRadius, attackTime;
    public Rigidbody2D rb;
    public Animator anim;
    public LayerMask playerMask;
    private int knockBackForce = 30;
    private float faceDirection, attackTimer = 0f, knockBackTimer = 0f;
    private Vector2 velocity;
    private Collider2D player;

    private void Start()
    {
        faceDirection = transform.localScale.x;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            player = collider;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && player == null)
            player = collider;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("IsWalking", false);
            attackTimer = 0f;
            player = null;
        }
    }

    private void FixedUpdate()
    {
        if (knockBackTimer <= 0f)
        {
            if (player != null)
            {
                float x = player.transform.position.x - rb.position.x;
                float y = player.transform.position.y - rb.position.y;

                anim.speed = 1f;

                if (Vector2.Distance(player.transform.position, rb.position) <= attackRadius && attackTimer <= 0f)
                {
                    Attack(x, y);
                }
                else
                {
                    Move(x, y);
                    attackTimer -= Time.deltaTime;
                }
            }
        }
        else
        {
            knockBackTimer -= Time.deltaTime;
            rb.linearVelocity = velocity;
        }
    }

    private void Attack(float x, float y)
    {
        rb.linearVelocity = Vector2.zero;
        attackTimer = attackTime;

        if (Mathf.Abs(x) >= Mathf.Abs(y))
            anim.Play("Attack Side");
        else if (y > 0)
            anim.Play("Attack Up");
        else
            anim.Play("Attack Down");
    }

    private void Move(float x, float y)
    {
        if (faceDirection > 0f && x < 0f || faceDirection < 0f && x > 0f)
        {
            faceDirection *= -1;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * faceDirection;
            transform.localScale = scale;
        }

        anim.SetBool("IsWalking", true);
        rb.linearVelocity = new Vector2(x, y).normalized * speed;
    }

    public void CauseDamage()
    {
        if (player != null)
        {
            if (Vector2.Distance(player.transform.position, rb.position) <= attackRadius)
            {
                player.GetComponent<PlayerKnockBack>()?.KnockBack(knockBackForce, rb.position.x, rb.position.y);
                player.GetComponent<PlayerHealth>()?.ChangeHealthPoints(-damage);
            }
        }
    }

    public void KnockBack(int force, float xPlayer, float yPlayer)
    {
        velocity = new Vector2(rb.position.x - xPlayer, rb.position.y - yPlayer).normalized * force;
        rb.linearVelocity = velocity;
        knockBackTimer = 0.25f;
        anim.speed = 0f;
    }
}
