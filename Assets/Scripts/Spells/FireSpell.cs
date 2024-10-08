using UnityEngine;

public abstract class FireSpell : Spell
{
    public float fireDamageMultiplier = 1.2f; // Множитель урона для огненных заклинаний

    // Переопределяем метод CastSpell для огненных заклинаний
    public override void CastSpell()
    {
        if (IsReady())
        {
            base.CastSpell(); // Вызовем базовую логику каста
            Debug.Log($"{spellName} deals extra fire damage!");
        }
        else
        {
            Debug.Log($"{spellName} is still on cooldown.");
        }
    }

    // Логика апгрейда огненных заклинаний
    public override void UpgradeSpell()
    {
        base.UpgradeSpell();
        fireDamageMultiplier += 0.1f; // Усиливаем урон с каждым уровнем
        Debug.Log($"{spellName} fire damage multiplier is now {fireDamageMultiplier}");
    }
}
