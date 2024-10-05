using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public Vector3 offset;   // Смещение камеры от игрока

    private void LateUpdate()
    {
        // Перемещаем камеру вслед за игроком, добавляя смещение
        transform.position = player.position + offset;
    }
}
