using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;    // Максимальное количество здоровья врага
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;   // При старте враг имеет полное здоровье
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // Уменьшаем текущее здоровье

        if (currentHealth <= 0)
        {
            Die();  // Если здоровье упало до нуля, враг умирает
        }
    }

    // Метод смерти врага
    private void Die()
    {
        // Можно добавить анимацию или логику смерти
        Debug.Log("Враг умер!");
        Destroy(gameObject);  // Уничтожаем объект врага
    }
}
