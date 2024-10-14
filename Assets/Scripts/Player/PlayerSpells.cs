using System.Collections.Generic;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    public List<SpellData> playerSpells;  // Список всех заклинаний игрока

    void Start()
    {
        // Пример инициализации заклинаний
        playerSpells = new List<SpellData>
        {
            new SpellData("Fireball", 1, 10f, 8),   // Уровень, кулдаун, макс. уровень
            new SpellData("Fireball2", 1, 10f, 8)
        };
    }

    // Метод для получения данных заклинания по имени
    public SpellData GetSpellData(string spellName)
    {
        return playerSpells.Find(spell => spell.spellName == spellName);
    }
}
