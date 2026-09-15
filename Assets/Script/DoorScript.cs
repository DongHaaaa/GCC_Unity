using System.Collections;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // 0 là đóng, 1 là mở
    private int doorState = 0;
    
    private Coroutine doorCoroutine;
    [SerializeField] private float speed = 5f; 
    [SerializeField] private Vector3 closedPosition; 
    [SerializeField] private Vector3 openPosition;   

    private void Start()
    {
        closedPosition = transform.position;
    }
    private IEnumerator CloseDoor()
    {
        //Cẩn thận chỗ lỏ này ko crash unity
        while (transform.position != closedPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, speed * Time.deltaTime);
            yield return null; 
        }
    }
    private IEnumerator OpenDoor()
    {
        while (transform.position != openPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, speed * Time.deltaTime);
            yield return null; 
        }
    }
    public void OpenAndCloseDoor()
    {
        if(doorState == 0)
        {
            doorState = 1;
            if (doorCoroutine != null) StopCoroutine(doorCoroutine);
            doorCoroutine = StartCoroutine(OpenDoor());
        }
        else
        {
            doorState = 0;
            if (doorCoroutine != null) StopCoroutine(doorCoroutine);
            doorCoroutine = StartCoroutine(CloseDoor());
        }
    }
}