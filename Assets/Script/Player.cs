using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    InputAction MoveAction;
    InputAction JumpAction;
    InputAction AttackAction;
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
        }
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
            Debug.Log("Attack Pressed");
        }
    }
}