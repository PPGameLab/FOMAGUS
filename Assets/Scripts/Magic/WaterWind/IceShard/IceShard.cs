using UnityEngine;

public class IceShard : SpellsWaterWind
{
    private GameObject targetEnemy;  // Цель снаряда
    private GameObject iceShardPrefab;  // Префаб снаряда

    public IceShard() : base("Ice Shard", 50f, 3f, 5)  // Название, базовый урон, кулдаун, макс уровень
    {
    }

    public override void Cast()
    {
        FindTarget();  // Ищем ближайшую цель

        if (targetEnemy != null)
        {
            // Логика запуска снаряда к цели
            GameObject iceShard = Object.Instantiate(Resources.Load("Magic/WaterWind/IceShardPrefab"), player.transform.position, Quaternion.identity) as GameObject;
            IceShardMovement iceShardMovement = iceShard.GetComponent<IceShardMovement>();

            if (iceShardMovement != null)
            {
                iceShardMovement.SetTarget(targetEnemy.transform.position, Damage);  // Передаём координаты цели
            }
        }
        else
        {
            Debug.Log("Цель не найдена.");
        }
    }

    public override void Upgrade(params object[] upgradeParams)
    {
        Level++;
        Damage += 10;  // Урон увеличивается на 10
        Cooldown = Mathf.Max(Cooldown - 0.2f, 1f);  // Кулдаун уменьшается, но не меньше 1 секунды
        Debug.Log($"{SpellName} улучшено до уровня {Level}: Урон {Damage}, Кулдаун {Cooldown}");
    }

    public override void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            targetEnemy = nearestEnemy;
            Debug.Log($"Цель {nearestEnemy.name} выбрана для {SpellName}");
        }
    }
}
