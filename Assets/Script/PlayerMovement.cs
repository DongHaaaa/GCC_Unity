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
    private InputAction MoveAction;
    private InputAction JumpAction;
    private Vector2 moveInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveAction = InputSystem.actions.FindAction("MoveAction");
        JumpAction = InputSystem.actions.FindAction("JumpAction");
    }
    /*void Awake()
    {
        coin.ResetCoin();
    }*/
    void Update()
    {
        moveInput = MoveAction.ReadValue<Vector2>();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        if (JumpAction.WasPressedThisFrame() && isGrounded)
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        Vector2 force = new Vector2(moveInput.x * moveSpeed, 0);
        rb.AddForce(force, ForceMode2D.Force);
        float clampedX = Mathf.Clamp(rb.linearVelocity.x, -maxSpeed, maxSpeed);
        rb.linearVelocity = new Vector2(clampedX, rb.linearVelocity.y);
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