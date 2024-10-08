using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image healthSlider;       // Полоска здоровья
    public TextMeshProUGUI healthText;   // Текст для отображения количества здоровья
    private PlayerStats playerStats; // Ссылка на статы игрока

    void Start()
    {
        // Получаем статы игрока
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats не найден!");
        }

        // Инициализируем начальные значения
        UpdateUI();
    }

    void Update()
    {
        // Обновляем UI на каждом кадре
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (playerStats != null)
        {
            // Рассчитываем процент заполнения полоски здоровья
            float healthPercentage = (float)playerStats.GetCurrentHealth() / playerStats.GetMaxHealth();
            healthSlider.fillAmount = healthPercentage; // Заполняем полоску здоровья

            // Обновляем текст здоровья
            healthText.text = $"HP: {playerStats.GetCurrentHealth()} / {playerStats.GetMaxHealth()}";
        }
    }
}
