using UnityEngine;

public class ExpObject : MonoBehaviour
{
    public float collectDistance = 0.5f; // Минимальное расстояние до игрока для сбора
    public int xpAmount = 10;       // Сколько опыта добавляет объект
    private Transform player;       // Ссылка на игрока
    private bool isBeingPulled = false;  // Флаг, который указывает, притягивается ли объект к игроку
    private float magnetSpeed = 5f;  // Скорость притяжения (передается с MagnetEffect)

    private void Start()
    {
        // Найдите игрока по тегу "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isBeingPulled)
        {
            // Двигаем объект к игроку с указанной скоростью
            transform.position = Vector2.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);

            // Проверяем, достаточно ли близко объект к игроку для сбора
            if (Vector2.Distance(transform.position, player.position) < collectDistance)
            {
                CollectExp();
            }
        }
    }

    // Метод, который активирует притягивание объекта и задает скорость притягивания
    public void ActivateMagnet(float speed)
    {
        isBeingPulled = true;
        magnetSpeed = speed;
    }

    // Логика при сборе объекта (добавляем опыт и уничтожаем объект)
    private void CollectExp()
    {
        Debug.Log("Опыт собран. Уничтожение объекта...");
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddExperience(xpAmount);
            Debug.Log("Опыт добавлен игроку: " + xpAmount);
        }
        else
        {
            Debug.LogWarning("PlayerStats не найден!");
        }

        Destroy(gameObject);
    }
}
