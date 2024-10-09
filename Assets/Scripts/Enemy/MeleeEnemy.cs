using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public GameObject[] lootPrefabs;    // Массив префабов лута, который может выпасть
    public int[] lootDropChances;     // Соответствующие шансы выпадения для каждого префаба
    public int health = 100;            // Количество здоровья врага
    private int currentHealth;
    public int damage = 10;             // Урон, наносимый игроку
    public float attackCooldown = 2f;   // Кулдаун между атаками
    public float moveSpeed = 2f;        // Скорость движения врага к игроку

    private Transform player;           // Ссылка на игрока
    private float lastAttackTime;       // Время последней атаки
    public EnemySpawner spawner; // Объявляем переменную для ссылки на спавнер

    private void Start()
    {
        currentHealth = health;
        // Найти игрока по тегу "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (lootPrefabs.Length != lootDropChances.Length)
        {
            Debug.LogError("Количество лутов и шансов на выпадение не совпадают!");
        }
    }

    private void Update()
    {
        // Движение врага к игроку
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    // Метод получения урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Враг получил урон: " + damage + ", текущее здоровье: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Враг умер!");
            Die();
        }
    }


    // Метод, который вызывается при смерти врага
    private void Die()
    {
        // Лог для проверки вызова метода смерти
        Debug.Log($"Враг {gameObject.name} умер");

        // Если враг находится в списке активных врагов, удаляем его
        if (spawner != null)
        {
            spawner.RemoveEnemy(gameObject);
            Debug.Log($"Враг {gameObject.name} удален из списка спавнера");
        }

        // Лог перед попыткой дропа лута
        DropLoot();

        // Уничтожаем объект врага
        Debug.Log($"Уничтожение объекта {gameObject.name}");
        Destroy(gameObject);
    }


    // Метод выпадения лута
    private void DropLoot()
    {
        // Проверяем, есть ли вообще что дропнуть
        if (lootPrefabs.Length > 0 && lootDropChances.Length == lootPrefabs.Length)
        {
            Debug.Log($"Попытка дропа лута у {gameObject.name}");

            for (int i = 0; i < lootPrefabs.Length; i++)
            {
                int chance = lootDropChances[i];
                if (Random.Range(0, 100) < chance)
                {
                    // Спавним лут
                    Instantiate(lootPrefabs[i], transform.position, Quaternion.identity);
                    Debug.Log($"Враг {gameObject.name} дропнул лут: {lootPrefabs[i].name}");
                    return;  // Один предмет дропнули — выходим из метода
                }
            }
            Debug.Log($"Враг {gameObject.name} не дропнул ничего.");
        }
        else
        {
            Debug.LogWarning($"Нет настроенных префабов для дропа лута у {gameObject.name}");
        }
    }


    // Метод, вызываемый при столкновении с игроком
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            if (playerStats != null && Time.time >= lastAttackTime + attackCooldown)
            {
                playerStats.TakeDamage(damage);  // Наносим урон игроку
                lastAttackTime = Time.time;  // Обновляем время последней атаки
            }
        }
    }
}
