using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int damage = 10;
    public float speed = 3f;
    public float attackCooldown = 2f; // кулдаун атаки
    private float lastAttackTime;
    
    private Transform player;

    private void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Движение к игроку
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Если враг достиг игрока
            if (Vector2.Distance(transform.position, player.position) < 0.5f)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time; // обновляем время последней атаки
                }
            }
        }
    }

    private void AttackPlayer()
    {
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(damage); // Наносим урон игроку
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);  // Уничтожаем врага
    }
}
