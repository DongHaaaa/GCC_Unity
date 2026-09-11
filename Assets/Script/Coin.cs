using UnityEngine;
using TMPro;
public class Coin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GayCheck gayCheck;
    private float timeGay = 0f;
    private float bdau = 0f;
    private void Start()
    {
        UpdateCoinText();
    }
    void Update()
    {
        timeGay += Time.deltaTime;
        if(bdau > 0f && timeGay - bdau > 2f)
        {
            gayCheck.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gayCheck.gameObject.SetActive(true);
            bdau = timeGay;
            Debug.Log("I'm Gay");
        }
    }
    public void ResetCoin()
    {
        gameObject.SetActive(true);
    }
    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "I'm Gay";
        }
    }
}