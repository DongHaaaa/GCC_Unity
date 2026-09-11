using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("Cấu hình di chuyển")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float speed = 2f;
    private bool movingRight = true;
    private void Start()
    {
        if(leftPoint != null) leftPoint.parent = null;
        if(rightPoint != null) rightPoint.parent = null;
    }
    private void Update()
    {
        if(leftPoint == null || rightPoint == null) return;
        if(movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(rightPoint.position.x, transform.position.y), speed * Time.deltaTime);
            if(Mathf.Abs(transform.position.x - rightPoint.position.x) < 0.05f)
            {
                Flip();
                movingRight = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftPoint.position.x, transform.position.y), speed * Time.deltaTime);
            if(Mathf.Abs(transform.position.x - leftPoint.position.x) < 0.05f)
            {
                Flip();
                movingRight = true;
            }
        }
    }
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        if(leftPoint != null && rightPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(leftPoint.position, 0.2f);
            Gizmos.DrawWireSphere(rightPoint.position, 0.2f);
            Gizmos.DrawLine(leftPoint.position, rightPoint.position);
        }
    }
}