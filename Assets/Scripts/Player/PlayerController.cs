using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;           // Ссылка на компонент Animator
    public float speed = 5f;            // Скорость персонажа
    public FloatingJoystick joystick;   // Ссылка на джойстик
    private Rigidbody2D rb;
    private Vector2 movement;
    public float deadZone = 0.1f;       // Порог для движения (джойстик должен отклоняться больше этого значения)

    // Аудио для воспроизведения музыки
    public AudioSource movementAudioSource; // Ссылка на компонент AudioSource

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D
        Debug.Log("PlayerController started. Initial setup complete.");

        // Проверяем, что AudioSource установлен
        if (movementAudioSource == null)
        {
            movementAudioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Получаем входные данные от джойстика
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        // Проверка, что джойстик отклонён больше, чем deadZone
        if (movement.magnitude > deadZone)
        {
            // Устанавливаем параметр Speed в аниматоре на величину скорости персонажа
            animator.SetFloat("Speed", movement.sqrMagnitude); // Используем квадрат длины вектора для оценки скорости

            // Воспроизводим музыку, если она ещё не воспроизводится
            if (!movementAudioSource.isPlaying)
            {
                movementAudioSource.Play();
            }
        }
        else
        {
            // Если джойстик не активен, останавливаем анимацию движения
            animator.SetFloat("Speed", 0);

            // Останавливаем музыку, если персонаж остановился
            if (movementAudioSource.isPlaying)
            {
                movementAudioSource.Stop();
            }
        }
    }

    void FixedUpdate()
    {
        // Двигаем персонажа только если джойстик отклонён больше порога
        if (movement.magnitude > deadZone)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }
}