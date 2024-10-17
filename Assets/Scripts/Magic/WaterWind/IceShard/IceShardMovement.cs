using UnityEngine;

public class IceShardMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 targetPosition;
    private float damage;

    public void SetTarget(Vector3 targetPos, float dmg)
    {
        targetPosition = targetPos;
        damage = dmg;

        // Поворот снаряда по направлению к цели
        Vector3 direction = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 222));
    }

    void Update()
    {
        // Движение снаряда к цели
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Уничтожить снаряд, если он достиг цели
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage((int)damage);
            }
            Destroy(gameObject);
        }
    }
}
