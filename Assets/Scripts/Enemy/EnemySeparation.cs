using UnityEngine;

public class EnemySeparation : MonoBehaviour
{
    public float separationForce = 1f;  // Сила, с которой враги будут отталкиваться друг от друга

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Рассчитываем направление, в котором нужно оттолкнуться
            Vector2 direction = transform.position - collision.transform.position;

            // Применяем силу отталкивания
            rb.AddForce(direction.normalized * separationForce);
        }
    }
}
