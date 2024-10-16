using UnityEngine;
using System.Collections.Generic;

public abstract class Spells
{
    public string SpellClass { get; protected set; }
    public string SpellName { get; protected set; }
    public int Level { get; protected set; }
    public float Cooldown { get; set; }
    public float Damage { get; protected set; }
    public int MaxLevel { get; protected set; }

    public Spells(string spellName, string spellClass, float baseDamage, float cooldown, int maxLevel)
    {
        SpellClass = spellClass;
        SpellName = spellName;
        Level = 1;
        Damage = baseDamage;
        Cooldown = cooldown;
        MaxLevel = maxLevel;
    }

    public abstract void Cast();

    public abstract void Upgrade(params object[] upgradeParams);

    public abstract void FindTarget();

    public virtual bool CanBeUpgraded()
    {
        return Level < MaxLevel;
    }

    public string GetSpellInfo()
    {
        return $"{SpellName} (Class: {SpellClass}) - Level: {Level}s";
    }
}