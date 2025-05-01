using UnityEngine;

public class PlayerKnockBack : MonoBehaviour
{
    public PlayerMovement movement;
    public Rigidbody2D rb;
    public Animator anim;
    private float knockBackTimer = 0f;
    private Vector2 velocity;

    private void FixedUpdate()
    {
        if (knockBackTimer <= 0f)
        {
            movement.UnlockedMovement();
        }
        else
        {
            knockBackTimer -= Time.deltaTime;
            rb.linearVelocity = velocity;
        }
    }

    public void KnockBack(int force, float xEnemy, float yEnemy)
    {
        velocity = new Vector2(rb.position.x - xEnemy, rb.position.y - yEnemy).normalized * force;
        movement.LockedMovement();
        rb.linearVelocity = velocity;
        knockBackTimer = 0.25f;
        anim.speed = 0f;
    }
}
