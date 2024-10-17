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
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, magnetRange);
        foreach (Collider2D hitCollider in hitColliders)
        {
            ExpObject xpObject = hitCollider.GetComponent<ExpObject>();
            if (xpObject != null)
            {
                xpObject.ActivateMagnet(magnetSpeed);
            }
        }
    }

    public void IncreaseMagnetRange(float amount)
    {
        magnetRange += amount;
        Debug.Log($"Magnet range increased by {amount}. New range: {magnetRange}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, magnetRange);
    }
}
