using UnityEngine;
using System.Collections.Generic;

public class LvlUpManager : MonoBehaviour
{
    public GameObject lvlUpPanel;  // Панель, которая показывает выбор бонусов
    public PlayerController playerController;  // Ссылка на контроллер игрока
    public MagicManager magicManager;  // Ссылка на менеджер магии
    public MagnetEffect magnetEffect;  // Ссылка на компонент магнита
    public int coinsAmount = 100;  // Количество монет за бонус

    private List<BonusBase> availableBonuses = new List<BonusBase>();
    private List<BonusBase> chosenBonuses = new List<BonusBase>();

    void Start()
    {
        // Инициализируем доступные бонусы
        InitializeBonuses();
    }

    // Инициализация доступных бонусов
    void InitializeBonuses()
    {
        // Пример создания бонусов
        availableBonuses.Add(new IncreaseSpeedBonus(playerController));
        availableBonuses.Add(new IncreaseMagnetRangeBonus(magnetEffect));
        SaveData playerData = SaveSystem.LoadGame();
        availableBonuses.Add(new GainCoinsBonus(playerData, coinsAmount));
    }

    // Метод для показа панели выбора бонусов
    public void ShowBonusPanel()
    {
        chosenBonuses.Clear();
        List<BonusBase> randomBonuses = GetRandomBonuses(3);
        foreach (BonusBase bonus in randomBonuses)
        {
            chosenBonuses.Add(bonus);
            // Логика отображения бонусов на UI-панели (например, с помощью кнопок)
            Debug.Log("Предложенный бонус: " + bonus.Description);
        }

        lvlUpPanel.SetActive(true);
        Time.timeScale = 0;  // Остановить время в игре
    }

    // Метод для скрытия панели и продолжения игры
    public void HideBonusPanel()
    {
        lvlUpPanel.SetActive(false);
        Time.timeScale = 1;  // Возобновить время в игре
    }

    // Применение выбранного бонуса
    public void ApplyBonus(int bonusIndex)
    {
        if (bonusIndex >= 0 && bonusIndex < chosenBonuses.Count)
        {
            chosenBonuses[bonusIndex].Apply();
            HideBonusPanel();
        }
    }

    // Получить случайный список бонусов
    List<BonusBase> GetRandomBonuses(int count)
    {
        List<BonusBase> randomBonuses = new List<BonusBase>();
        List<BonusBase> tempBonuses = new List<BonusBase>(availableBonuses);

        for (int i = 0; i < count; i++)
        {
            if (tempBonuses.Count == 0) break;

            int randomIndex = Random.Range(0, tempBonuses.Count);
            randomBonuses.Add(tempBonuses[randomIndex]);
            tempBonuses.RemoveAt(randomIndex);
        }

        return randomBonuses;
    }
}
