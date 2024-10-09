using UnityEngine;

public class HPBarFollow : MonoBehaviour
{
    public Transform player;  // Ссылка на объект игрока
    public RectTransform hpBar;  // Ссылка на RectTransform полоски здоровья
    public Vector3 offset;   // Смещение от позиции игрока

    void Update()
    {
        // Обновляем позицию полоски здоровья в зависимости от позиции игрока
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position + offset);
        hpBar.position = screenPos;
    }
}
