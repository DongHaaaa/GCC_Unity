using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    [SerializeField] private bool ShowGizmos = true;
    [SerializeField] private int H = 5;
    [SerializeField] private int C = 5;
    [SerializeField] private Vector2 cellSize = new Vector2(1f, 1f);
    [SerializeField] private Vector2 spacing = new Vector2(0.2f, 0.2f);
    private void OnDrawGizmos()
    {
        if (!ShowGizmos) return;
        Gizmos.color = Color.pink;
        float stepX = cellSize.x + spacing.x;
        float stepY = cellSize.y + spacing.y;
        float startX = -((C - 1) * stepX) / 2f;
        float startY = -((H - 1) * stepY) / 2f;
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < C; j++)
            {
                float posX = startX + j * stepX;
                float posY = startY + i * stepY;
                Vector3 cellPosition = new Vector3(posX, posY, 0);
                Gizmos.DrawWireCube(cellPosition, cellSize);
            }
        }
    }
}