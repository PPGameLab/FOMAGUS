using UnityEngine;

public abstract class SpellsWaterWind : Spells
{
    protected Transform player;  // Ссылка на игрока
    public SpellsWaterWind(string spellName, float baseDamage, float cooldown, int maxLevel)
        : base(spellName, "Water & Wind", baseDamage, cooldown, maxLevel) // Добавляем maxLevel
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  // Найти игрока по тэгу
    }

    public override void Upgrade(params object[] upgradeParams)
    {
        Level++;
        Damage += Level * 2;  
        Cooldown = Mathf.Max(Cooldown - 0.2f, 1f); 
    }

    // Переопределяем метод FindTarget
    public override void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            // Проверяем, находится ли враг на экране
            if (IsOnScreen(enemy.transform.position))
            {
                float distanceToEnemy = Vector3.Distance(enemy.transform.position, player.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        if (nearestEnemy != null)
        {
            Debug.Log($"Найдена ближайшая цель к игроку: {nearestEnemy.name}");
        }
        else
        {
            Debug.Log("Цель не найдена на экране");
        }
    }

    public override void Cast()
    {
        // Общая логика кастования для заклинаний этой школы (например, создание снаряда)
        Debug.Log("Casting spell from Water & Wind school: " + SpellName);
    }
    // Метод для проверки, находится ли враг на экране
    private bool IsOnScreen(Vector3 position)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(position);
        return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1 && screenPoint.z > 0;
    }
}
