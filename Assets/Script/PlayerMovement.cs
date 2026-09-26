using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float maxSpeed = 7f;

    [Header("GroundCheck Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private InputAction JumpAction;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        JumpAction = InputSystem.actions.FindAction("JumpAction");
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (JumpAction.WasPressedThisFrame() && isGrounded)
        {
            Jump();
        }
    }

    public void Move(float moveX)
    {
        Vector2 force = new Vector2(moveX * moveSpeed, 0);
        rb.AddForce(force, ForceMode2D.Force);

        float clampedX = Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed);
        rb.linearVelocity = new Vector2(clampedX, rb.linearVelocity.y);
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

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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
