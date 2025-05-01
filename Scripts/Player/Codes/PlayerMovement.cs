using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool movementBlocked = false;
    private int speed = 5, faceDirection = 1;
    private InputManager controls;
    public Rigidbody2D rb;
    public Animator anim;

    private void Start()
    {
        controls = InputManager.Instantiate();
    }

    private void FixedUpdate()
    {
        if (!movementBlocked)
            Move();
    }

    private void Move()
    {
        int accelerator = controls.IsRunning() ? 2 : 1;
        float movX = controls.GetMovX();
        float movY = controls.GetMovY();

        if (faceDirection > 0f && movX < 0f || faceDirection < 0f && movX > 0f)
        {
            faceDirection *= -1;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * faceDirection;
            transform.localScale = scale;
        }

        anim.speed = accelerator;
        anim.SetBool("IsWalking", movX != 0f || movY != 0f);
        rb.linearVelocity = new Vector2(movX * speed * accelerator, movY * speed * accelerator);
    }

    public void LockedMovement()
    {
        this.movementBlocked = true;
    }

    public void UnlockedMovement()
    {
        movementBlocked = false;
    }
}
