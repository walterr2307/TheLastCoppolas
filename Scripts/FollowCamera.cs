using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;

    private void Update()
    {
        if (target != null)
        {
            Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Lerp(transform.position, newPos, followSpeed * Time.deltaTime);
        }
    }
}
