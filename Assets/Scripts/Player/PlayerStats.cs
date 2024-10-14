using UnityEngine;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
    public int Level = 1;       // Текущий уровень
    public int CurrentEXP = 0;   // Текущий опыт
    public int RequiredEXP = 100; // Сколько нужно опыта для следующего уровня

    // FIGHT PARAMS
    public int FightStrength         = 1;
    public int FightAgility          = 1;
    public int FightIntelligence     = 1;
    public int FightWisdom           = 1;
    public int FightVitality         = 1;
    public int FightDurability       = 1;
    public int FightLuck             = 1;
    public int FightGreed            = 1;

    public int maxHealth = 100;
    public int currentHealth;

    // ITEM PARAMS
    public int itemBonusMagnet = 0;

    // BATTLE PARAMS
    public List<SpellData> availableSpells;  // Список доступных заклинаний

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

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    // Функция повышения уровня
    private void LevelUp()
    {
        Level++;
        RequiredEXP += 100 + Level * 10; // Увеличение опыта, необходимого для следующего уровня
        Debug.Log("Уровень повышен! Новый уровень: " + Level);

        // Открываем панель повышения уровня
        LvlUpPanelController lvlUpPanel = FindObjectOfType<LvlUpPanelController>();
        lvlUpPanel.ShowPanel();
    }

    private void Start()
    {
        LoadBaseParamsFromSaveData();
        CalculateBattleParams();

        // Инициализация списка заклинаний
        availableSpells = new List<SpellData>
        {
            new SpellData("Fireball", 0, 5f, 8),    // Название, уровень, кулдаун, макс уровень
            new SpellData("Fireball2", 0, 7f, 8)    // Добавляем заклинание Fireball2
        };
    }


    public void UpgradeSpell(string spellName)
    {
        SpellData spell = availableSpells.Find(s => s.spellName == spellName);
        if (spell != null && spell.level < spell.maxLevel)
        {
            spell.level++;
            Debug.Log(spell.spellName + " улучшено до уровня: " + spell.level);
        }
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
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;   // Уменьшаем текущее здоровье

        if (currentHealth <= 0)
        {
            Die();  // Если здоровье упало до нуля или ниже, вызываем смерть игрока
        }
    }

    // Метод смерти игрока
    private void Die()
    {
        Debug.Log("Player died!");  // Можно добавить логику для конца игры или перезапуска
    }

    // Управление заклинаниями
    void Update()
    {
        foreach (SpellData spell in availableSpells)
        {
            if (spell.level > 0 && Time.time >= spell.nextCastTime)
            {
                CastSpell(spell);
                spell.nextCastTime = Time.time + spell.cooldown;
            }
        }
    }

    void CastSpell(SpellData spell)
    {
        // Логика кастования заклинания в зависимости от его типа
        if (spell.spellName == "Fireball")
        {
            Instantiate(Resources.Load("FireballSpell"), transform.position, Quaternion.identity);
        }
        else if (spell.spellName == "Fireball2")
        {
            Instantiate(Resources.Load("Fireball2"), transform.position, Quaternion.identity);
        }

        Debug.Log(spell.spellName + " заклинание кастуется!");
    }
}
