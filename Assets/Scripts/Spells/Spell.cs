using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    public string spellName;
    public int level = 1;
    public float cooldown = 2f; // Время перезарядки
    protected float lastCastTime; // Время последнего каста

    public virtual void CastSpell()
    {
        // Логика для кастования заклинания (зависит от типа заклинания)
        Debug.Log($"{spellName} was cast!");
        lastCastTime = Time.time; // Запоминаем время последнего каста
    }

    // Проверка, готово ли заклинание к повторному использованию
    public bool IsReady()
    {
        return Time.time >= lastCastTime + cooldown;
    }

    // Логика для апгрейда заклинания
    public virtual void UpgradeSpell()
    {
        level++;
        Debug.Log($"{spellName} upgraded to level {level}");
    }
}
