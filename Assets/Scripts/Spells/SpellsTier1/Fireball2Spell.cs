using UnityEngine;

public class FireBall2Spell : MonoBehaviour
{
    public float speed = 5f;           // Скорость полёта заклинания
    public GameObject explosionPrefab; // Префаб взрыва
    public Vector3 targetPosition;     // Целевая позиция, куда летит заклинание
    public float explosionRadius = 3f; // Радиус взрыва
    public int damage = 20;            // Урон взрыва
    public LayerMask enemyLayer;       // Слой врагов

    private void Start()
    {
        // Установим направление движения к цели
        Vector3 direction = (targetPosition - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    private void Update()
    {
        // Проверка: достигли ли мы цели
        if (Vector3.Distance(transform.position, targetPosition) <= 0.1f)
        {
            Explode();  // Если достигли цели, вызываем взрыв
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Если снаряд попал во врага, вызываем взрыв на месте столкновения
            Explode();
        }
    }

    private void Explode()
    {
        // Спавним префаб взрыва на текущей позиции
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Explosion Prefab is not assigned!");
        }

        // Находим всех врагов в радиусе взрыва
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);

        // Применяем урон всем врагам в радиусе взрыва
        foreach (Collider2D enemy in enemiesHit)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Уничтожаем снаряд
        Destroy(gameObject);
    }

    // Отрисовка радиуса взрыва в редакторе Unity (опционально, для удобства)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
