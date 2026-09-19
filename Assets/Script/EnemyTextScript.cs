using UnityEngine;

public class EnemyTextScript : MonoBehaviour
{
    [SerializeField] private GameObject EnemyMove;
    void Update()
    {
        gameObject.transform.position = EnemyMove.transform.position + new Vector3(0, 1.5f, 0);
    }
}
