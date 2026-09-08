using UnityEngine;
using TMPro;
public class Coin : MonoBehaviour
{
    private int coinCount = 0;
    [SerializeField] private TextMeshProUGUI coinText;
    private void Start()
    {
        UpdateCoinText();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            coinCount++;
            Debug.Log("Total Coins: " + coinCount);
            UpdateCoinText();
            gameObject.SetActive(false);
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
            coinText.text = "Coins: " + coinCount.ToString();
        }
    }
}