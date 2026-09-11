using System.Collections;
using UnityEngine;
using TMPro;

public class EnemyTrigger : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject TextObject;
    [SerializeField] private float displayDuration = 2f;
    private Coroutine hideCoroutine;
    private void Start()
    {
        if(TextObject != null)
        {
            TextObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player đã chạm vào Enemy!");
            ShowText();
        }
    }
    private void ShowText()
    {
        if(TextObject == null) return;
        TextObject.SetActive(true);
        if(hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }
        hideCoroutine = StartCoroutine(HideTextRoutine());
    }
    private IEnumerator HideTextRoutine()
    {
        yield return new WaitForSeconds(displayDuration);
        if(TextObject != null)
        {
            TextObject.SetActive(false);
        }
    }
}