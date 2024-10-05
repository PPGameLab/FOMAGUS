using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FloatingJoystick joystick; // Ссылка на ваш джойстик
    public float speed = 50f; // Скорость движения персонажа
    private Rigidbody2D rb; // Ссылка на Rigidbody2D для движения персонажа

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем Rigidbody2D компонента игрока
    }

    private void Update()
    {
        // Получаем направление ввода от джойстика
        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);
        Debug.Log("Joystick Direction: " + direction);

        // Двигаем персонажа с помощью Rigidbody2D
        rb.velocity = direction * speed;
    }
}
