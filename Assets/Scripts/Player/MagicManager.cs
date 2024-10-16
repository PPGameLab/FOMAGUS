using UnityEngine;
using System.Collections.Generic;

public class MagicManager : MonoBehaviour
{
    public List<Spells> activeSpells = new List<Spells>();  // Список активных заклинаний
    public Transform player;  // Ссылка на объект игрока
    private float nextCastTime = 0f;  // Время следующего каста

    void Start()
    {
        // Инициализация доступных заклинаний для игрока
        InitializeSpells();
    }

    void Update()
    {
        HandleSpellCasting();  // Проверяем возможность каста
    }

    // Метод инициализации списка активных заклинаний
    private void InitializeSpells()
    {
        // Например, изначально игрок получает несколько базовых заклинаний
        AddSpell(new IceShard());  // Добавляем заклинание IceShard
        // Можно добавлять больше заклинаний по мере прогресса
    }

    // Добавляем заклинание в список активных
    public void AddSpell(Spells spell)
    {
        if (!activeSpells.Contains(spell))
        {
            activeSpells.Add(spell);
        }
    }

    // Обработка кастов заклинаний
    private void HandleSpellCasting()
    {
        foreach (Spells spell in activeSpells)
        {
            // Проверяем, что заклинание готово к использованию (если кулдаун прошел)
            if (spell.Level > 0 && Time.time >= nextCastTime)
            {
                spell.Cast();  // Кастуем заклинание

                // Обновляем кулдаун для заклинания
                nextCastTime = Time.time + spell.Cooldown;
            }
        }
    }

    // Метод для увеличения уровня заклинания или добавления нового
    public void UpgradeOrAddSpell(string spellName)
    {
        Spells spell = activeSpells.Find(s => s.SpellName == spellName);

        if (spell != null && spell.CanBeUpgraded())
        {
            spell.Upgrade();  // Если заклинание уже есть, улучшаем его
        }
        else
        {
            // Если заклинание не найдено, добавляем новое в активный список
            switch (spellName)
            {
                case "Ice Shard":
                    AddSpell(new IceShard());
                    break;
                // Можно добавить логику для других заклинаний
                default:
                    Debug.LogError($"Заклинание {spellName} не найдено!");
                    break;
            }
        }
    }
}
