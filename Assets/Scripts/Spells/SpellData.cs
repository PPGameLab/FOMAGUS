using UnityEngine;

[System.Serializable]
public class SpellData
{
    public string spellName;
    public int level = 0;           // Уровень заклинания
    public int maxLevel = 8;        // Максимальный уровень заклинания
    public float baseDamage = 10f;  // Базовый урон заклинания
    public float cooldown = 5f;     // Кулдаун заклинания
    public float nextCastTime;      // Время следующего каста

    public SpellData(string name, int level, float cooldown, int maxLevel)
    {
        this.spellName = name;
        this.level = level;
        this.cooldown = cooldown;
        this.maxLevel = maxLevel;
        this.nextCastTime = 0f; // Инициализация времени каста
    }

    // Метод для получения текущего урона
    public float GetDamage()
    {
        return baseDamage + (level * 5);
    }

    // Метод для получения кулдауна с учётом уровня
    public float GetCooldown()
    {
        return Mathf.Max(cooldown - (level * 0.5f), 1f);
    }
}
