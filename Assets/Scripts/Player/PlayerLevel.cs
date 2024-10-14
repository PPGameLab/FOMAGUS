using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public int currentLevel = 1;
    public int experience = 0;
    public int experienceToNextLevel = 100;
    public LvlUpPanelController lvlUpPanelController;

    void Update()
    {
        if (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;
        experience = 0;  // Сброс опыта после повышения уровня
        experienceToNextLevel += 50;  // Увеличиваем требуемый опыт для следующего уровня
        Debug.Log("Уровень повышен! Текущий уровень: " + currentLevel);

        // Открываем панель выбора бонусов
        lvlUpPanelController.ShowPanel();
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        Debug.Log("Опыт добавлен: " + amount);
    }
}
