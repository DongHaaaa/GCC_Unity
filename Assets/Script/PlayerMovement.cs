using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float maxSpeed = 7f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 18f;

    [Header("GroundCheck Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private float defaultGravityScale;

    public bool IsGrounded =>
        Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

    public float FacingDirection => transform.localScale.x >= 0 ? 1f : -1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
    }

    public void Move(float moveX)
    {
        Vector2 force = new Vector2(moveX * moveSpeed, 0);
        rb.AddForce(force, ForceMode2D.Force);

        float clampedX = Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed);
        rb.linearVelocity = new Vector2(clampedX, rb.linearVelocity.y);
    }

    public void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void BeginDash(float direction)
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(direction * dashSpeed, 0f);
    }

    public void ContinueDash(float direction)
    {
        rb.linearVelocity = new Vector2(direction * dashSpeed, 0f);
    }

    public void EndDash()
    {
        rb.gravityScale = defaultGravityScale;
    }

    public void StopHorizontal()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void Flip(float moveX)
    {
        if (transform.localScale.x > 0 && moveX < 0 ||
            transform.localScale.x < 0 && moveX > 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
