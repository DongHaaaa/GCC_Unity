using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputAction MoveAction;
    InputAction JumpAction;
    InputAction AttackAction;
    [SerializeField] private Animator anim;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Awake()
    {
        MoveAction = InputSystem.actions.FindAction("MoveAction");
        JumpAction = InputSystem.actions.FindAction("JumpAction");
        AttackAction = InputSystem.actions.FindAction("AttackAction");
    }
    void Update()
    {
        if (MoveAction.IsPressed())
        {
            Debug.Log(MoveAction.ReadValue<Vector2>());
            anim.SetBool(name: "IsRunning", true);
        }
        else anim.SetBool(name: "IsRunning", false);
        if (JumpAction.WasPressedThisFrame())
        {
            Debug.Log("Jump Pressed");
        }
        if (JumpAction.IsPressed())
        {
            Debug.Log("Jump Hold");
        }
        if (JumpAction.WasReleasedThisFrame())
        {
            Debug.Log("Jump Release");
        }
        if (AttackAction.IsPressed())
        {
            anim.SetBool("IsAttacking", true);
            Debug.Log("Attack Pressed");
        }
        float Vy= rb.linearVelocity.y;
        if(Vy < 0)
        {
            anim.SetBool("IsFalling", true);
            anim.SetBool("IsJumping", false);
            anim.SetBool("OnGround", false);
        }
        else if(Vy > 0)
        {
            anim.SetBool("IsJumping", true);
            anim.SetBool("IsFalling", false);
            anim.SetBool("OnGround", false);
        }
        else
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsFalling", false);
            anim.SetBool("OnGround", true);
        }
    }
    public void OnAttackEnd()
    {
        anim.SetBool("IsAttacking", false);
    }
}