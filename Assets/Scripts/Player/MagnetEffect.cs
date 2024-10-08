using UnityEngine;

public class MagnetEffect : MonoBehaviour
{
    public float magnetRange = 5f;  // Радиус действия магнита (задается при старте боя)
    public float magnetSpeed = 5f;  // Скорость притягивания объектов опыта
    private PlayerStats playerStats;  // Ссылка на статы игрока

    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats не найден на объекте игрока!");
        }
        else
        {
            // Инициализируем радиус магнита на основе статов игрока
            magnetRange = playerStats.magnetRange;
        }
    }

    private void Update()
    {
        // Находим все объекты опыта в радиусе магнита
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, magnetRange);
        foreach (Collider2D hitCollider in hitColliders)
        {
            ExpObject xpObject = hitCollider.GetComponent<ExpObject>();
            if (xpObject != null)
            {
                // Активируем магнет на объекте опыта
                xpObject.ActivateMagnet(magnetSpeed);  // Передаем скорость притягивания
            }
        }
    }

    // Рисуем радиус магнита в редакторе для удобства настройки
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, magnetRange);
    }
}
