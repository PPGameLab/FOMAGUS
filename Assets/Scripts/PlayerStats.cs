using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int Level = 1;       // Текущий уровень
    public int CurrentEXP = 0;   // Текущий опыт
    public int RequiredEXP = 100; // Сколько нужно опыта для следующего уровн

    // FIGHT PARAMS
    public int FightStrength         = 1;
    public int FightAgility          = 1;
    public int FightIntelligence     = 1;
    public int FightWisdom           = 1;
    public int FightVitality         = 1;
    public int FightDurability       = 1;
    public int FightLuck             = 1;
    public int FightGreed            = 1;

    // ITEM PARAMS
    public int itemBonusMagnet = 0;

    // BATTLE PARAMS

    public float magnetRange = 0;

    // Функция для добавления опыта
    public void AddExperience(int Exp)
    {
        CurrentEXP += Exp;
        CheckLevelUp();
    }

    // Проверка, нужно ли повысить уровень
    private void CheckLevelUp()
    {
        if (CurrentEXP >= RequiredEXP)
        {
            CurrentEXP -= RequiredEXP;
            LevelUp();
        }
    }

    // Функция повышения уровня
    private void LevelUp()
    {
        Level++;
        RequiredEXP += 100 + Level * 10; // Увеличение опыта, необходимого для следующего уровня
        Debug.Log("Уровень повышен! Новый уровень: " + Level);
    }

    private void Start()
    {
        LoadBaseParamsFromSaveData();
        CalculateBattleParams();
    }

    public void LoadBaseParamsFromSaveData()
    {
        SaveData playerData = SaveSystem.LoadGame();
        FightStrength       = playerData.Strength;
        FightAgility        = playerData.Agility;
        FightIntelligence   = playerData.Intelligence;
        FightWisdom         = playerData.Wisdom;
        FightVitality       = playerData.Vitality;
        FightDurability     = playerData.Durability;
        FightLuck           = playerData.Luck;
        FightGreed          = playerData.Greed;
    }

    public void CalculateBattleParams()
    {
        magnetRange = 20 + FightGreed + itemBonusMagnet;
    }

    
}
