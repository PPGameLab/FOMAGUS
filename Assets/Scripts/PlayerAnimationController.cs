using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator; // Ссылка на компонент Animator
    public float speed = 5f;  // Скорость персонажа
    public FloatingJoystick joystick; // Ссылка на твой джойстик
    private Rigidbody2D rb;
    private Vector2 movement;
    public float deadZone = 0.1f; // Порог для движения (джойстик должен отклоняться больше этого значения)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
        Debug.Log("PlayerController started. Initial setup complete.");
    }

    void Update()
    {
        // Получаем входные данные от джойстика
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        Debug.Log($"Joystick Input - Horizontal: {movement.x}, Vertical: {movement.y}");

        // Проверка, что джойстик отклонён больше, чем deadZone
        if (movement.magnitude > deadZone)
        {
            // Устанавливаем параметр Speed в аниматоре на величину скорости персонажа
            animator.SetFloat("Speed", movement.sqrMagnitude); // Используем квадрат длины вектора для оценки скорости
            Debug.Log($"Player is moving. Speed: {movement.sqrMagnitude}");
        }
        else
        {
            // Если джойстик не активен, останавливаем анимацию движения
            animator.SetFloat("Speed", 0);
            Debug.Log("Player is idle. Joystick in dead zone.");
        }
    }

    void FixedUpdate()
    {
        // Двигаем персонажа только если джойстик отклонён больше порога
        if (movement.magnitude > deadZone)
        {
            //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            rb.MovePosition(rb.position + movement * speed );
            Debug.Log("Player moved.");
            Debug.Log("Vector: " + movement);
            Debug.Log("Speed: " + speed);
        }
        else
        {
            Debug.Log("No movement. Player is stationary.");
        }
    }
}