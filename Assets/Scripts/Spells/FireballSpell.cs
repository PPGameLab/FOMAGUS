using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    public float maxLifetime = 5f;

    private Transform target;

    void Start()
    {
        // Найти ближайшую цель с тагом "Enemy"
        FindTarget();
        Destroy(gameObject, maxLifetime); // Уничтожить объект через заданное время
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Движение к цели
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Получение компонента MeleeEnemy вместо EnemyHealth
            MeleeEnemy enemy = collision.GetComponent<MeleeEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // Наносим урон врагу
            }

            // Уничтожить огненный шар при столкновении
            Destroy(gameObject);
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && IsOnScreen(enemy.transform.position))
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
    }

    bool IsOnScreen(Vector3 position)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}
