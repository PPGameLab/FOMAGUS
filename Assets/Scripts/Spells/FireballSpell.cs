using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public float speed = 5f;           // Скорость огненного шара
    public int damage = 10;            // Урон от огненного шара
    public float searchRadius = 10f;   // Радиус поиска врагов
    public float maxTravelDistance = 20f; // Максимальная дистанция полёта
    private Transform targetEnemy;     // Цель (случайный враг)
    private Vector2 startPosition;     // Начальная позиция огненного шара

    void Start()
    {
        startPosition = transform.position; // Запоминаем стартовую позицию огненного шара
        FindRandomEnemy(); // Ищем случайного врага при запуске
        MoveToTarget();    // Начинаем движение
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            // Двигаем огненный шар к выбранной цели
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.position, speed * Time.deltaTime);

            // Если огненный шар достиг врага
            if (Vector2.Distance(transform.position, targetEnemy.position) < 0.1f)
            {
                HitEnemy();
            }
        }

        // Проверяем, пролетел ли огненный шар слишком далеко
        if (Vector2.Distance(startPosition, transform.position) > maxTravelDistance)
        {
            Explode(); // Если пролетел слишком далеко, уничтожаем шар
        }
    }

    // Поиск случайного врага в радиусе
    private void FindRandomEnemy()
    {
        // Находим всех врагов в радиусе
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, searchRadius);
        Collider2D[] visibleEnemies = System.Array.FindAll(enemiesInRange, c => c.CompareTag("ENEMY"));

        // Если враги найдены, выбираем случайного
        if (visibleEnemies.Length > 0)
        {
            int randomIndex = Random.Range(0, visibleEnemies.Length);
            targetEnemy = visibleEnemies[randomIndex].transform;
        }
    }

    // Движение к цели
    private void MoveToTarget()
    {
        if (targetEnemy != null)
        {
            Vector2 direction = (targetEnemy.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
        else
        {
            // Если врагов нет, направляем огненный шар вперед
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        }
    }

    // Метод обработки попадания во врага
    private void HitEnemy()
    {
        if (targetEnemy != null)
        {
            // Наносим урон врагу, если у него есть компонент, отвечающий за здоровье
            EnemyHealth enemyHealth = targetEnemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // Наносим урон
            }
        }
        Destroy(gameObject); // Уничтожаем огненный шар
    }

    // Взрыв, если огненный шар пролетел слишком далеко
    private void Explode()
    {
        Debug.Log("Огненный шар взорвался из-за слишком большой дистанции");
        Destroy(gameObject); // Уничтожаем огненный шар
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            HitEnemy();
        }
    }
}
