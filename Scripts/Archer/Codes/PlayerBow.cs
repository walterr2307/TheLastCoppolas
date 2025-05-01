using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public int speed;
    public float shootTime;
    public Rigidbody2D rb;
    public Animator anim;
    public Transform bowPoint;
    public GameObject arrowPrefab;

    private int faceDirection;
    private float x, y, shootTimer = 0f;
    private InputManager controls;

    private void Start()
    {
        controls = InputManager.Instantiate();
        faceDirection = (int)transform.localScale.x;
    }

    private void FixedUpdate()
    {
        if (controls.IsAttacking() && shootTimer <= 0f)
        {
            anim.speed = 1f;
            Shoot();
        }
        else
        {
            shootTimer -= Time.deltaTime;
            Move();
        }
    }

    private void Move()
    {
        int acelarator = controls.IsRunning() ? 2 : 1;
        x = controls.GetMovX();
        y = controls.GetMovY();

        if (x < 0f && faceDirection > 0f || x > 0f && faceDirection < 0f)
        {
            faceDirection *= -1;
            float scaleX = Mathf.Abs(transform.localScale.x) * faceDirection;
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }

        anim.speed = acelarator;
        anim.SetBool("IsWalking", x != 0f || y != 0f);
        rb.linearVelocity = new Vector2(x, y) * speed * acelarator;
    }

    public void Shoot()
    {
        Arrow arrow = Instantiate(arrowPrefab, bowPoint.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Rotate(x, y, transform.localScale.x);
        shootTimer = shootTime;
    }
}
