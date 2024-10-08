using UnityEngine;

public class ExpObject : MonoBehaviour
{
    public delegate void ExpCollected();
    public event ExpCollected OnCollected;  // Событие для сбора

    public float magnetSpeed = 5f;
    public float collectDistance = 1f;
    public int xpAmount = 10;
    private Transform player;
    private bool isBeingPulled = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (isBeingPulled)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, magnetSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.position) < collectDistance)
            {
                CollectExp();
            }
        }
    }

    public void ActivateMagnet(float speed)
    {
        magnetSpeed = speed;
        isBeingPulled = true;
    }

    private void CollectExp()
    {
        // Добавляем опыт игроку
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddExperience(xpAmount);
        }

        // Вызываем событие, если есть подписчики
        if (OnCollected != null)
        {
            OnCollected.Invoke();
        }

        // Уничтожаем объект после сбора
        Destroy(gameObject);
    }
}
