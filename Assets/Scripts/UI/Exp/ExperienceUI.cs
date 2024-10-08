using UnityEngine;
using UnityEngine.UI;
using TMPro; // Нужно для работы с TextMeshPro

public class ExperienceUI : MonoBehaviour
{
    public Image experienceSlider;
    public TMP_Text levelText; // Используем TMP_Text для TextMeshPro
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats не найден!");
        }

        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (playerStats != null)
        {
            float expPercentage = (float)playerStats.CurrentEXP / playerStats.RequiredEXP;
            experienceSlider.fillAmount = expPercentage;

            levelText.text = " " + playerStats.Level;
        }
    }
}
