using UnityEngine;
using UnityEngine.InputSystem;
public class CheckButton : MonoBehaviour
{
    [SerializeField] private GameObject checkButtonText;
    [SerializeField] private DoorScript OpenAndCloseDoor;
    InputAction PressButton;
    private bool isPlayerInRange = false;
    void Start()
    {
        checkButtonText.SetActive(false);
    }
    void Awake()
    {
        PressButton = InputSystem.actions.FindAction("PressButton");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ktra xem player vào vùng button chưa
        if(collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            //Player lại gần button thì hiện chữ lên
            checkButtonText.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Ktra xem player ra khỏi vùng button chưa
        if(collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            //Player ra khỏi button thì ẩn chữ
            checkButtonText.SetActive(false);
        }
    }
    void Update()
    {
        //Player nhấn F thì mở hoặc đóng cửa
        if(isPlayerInRange && PressButton.WasPressedThisFrame())
        {
           // Gọi hàm để mở hoặc đóng cửa
           Debug.Log("Nhấn F rồi");
           OpenAndCloseDoor.OpenAndCloseDoor();
        }
    }
}
