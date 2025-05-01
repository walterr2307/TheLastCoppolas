using UnityEngine;

public class Arrow : MonoBehaviour
{
   public float speed, lifeTime;
   public Rigidbody2D rb;
   private Vector2 direction;

   private void Start()
   {
      direction = Vector2.right;
      Destroy(gameObject, lifeTime);
   }

   private void FixedUpdate()
   {
      rb.linearVelocity = direction * speed;
   }

   public void Rotate(float x, float y, float faceDirection)
   {
      if (x != 0f || y != 0f)
      {
         float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
         direction = new Vector2(x, y).normalized;
         transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
      }
      else if (faceDirection < 0)
      {
         direction *= -1f;
      }
   }
}
