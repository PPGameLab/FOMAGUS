using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPrefab
{
    public GameObject prefab;    // Префаб врага
    public float spawnChance;    // Шанс появления
}

public class EnemySpawner : MonoBehaviour
{
    public EnemyPrefab[] enemyPrefabs; // Массив врагов с их шансами появления
    public float spawnInterval = 5f;   // Интервал спавна врагов
    public float minSpawnDistance = 3f;  // Минимальное расстояние спавна от экрана
    public float maxSpawnDistance = 8f;  // Максимальное расстояние спавна от экрана
    public int maxEnemies = 10;        // Максимальное количество врагов
    public Transform player;           // Ссылка на игрока

    private Camera mainCamera;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        // Получаем основную камеру
        mainCamera = Camera.main;

        // Начинаем спавн врагов с определённым интервалом
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Проверяем, если количество врагов меньше, чем максимум
            if (spawnedEnemies.Count < maxEnemies)
            {
                // Спавним врага
                SpawnEnemy();
            }

            // Ждем перед следующим спавном
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Выбираем префаб врага на основе вероятностей
        GameObject enemyPrefab = GetRandomEnemyPrefab();

        if (enemyPrefab != null)
        {
            // Вычисляем позицию спавна
            Vector3 spawnPosition = GetSpawnPosition();

            // Создаём врага и добавляем в список
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(enemy);
        }
    }

    GameObject GetRandomEnemyPrefab()
    {
        // Суммируем все шансы спавна
        float totalChance = 0f;
        foreach (var enemyPrefab in enemyPrefabs)
        {
            totalChance += enemyPrefab.spawnChance;
        }

        // Получаем случайное значение между 0 и суммой шансов
        float randomValue = Random.Range(0f, totalChance);

        // Ищем подходящий префаб на основе случайного значения
        float currentChance = 0f;
        foreach (var enemyPrefab in enemyPrefabs)
        {
            currentChance += enemyPrefab.spawnChance;
            if (randomValue <= currentChance)
            {
                return enemyPrefab.prefab;
            }
        }

        return null;
    }

    Vector3 GetSpawnPosition()
    {
        Vector3 playerPosition = player.position;

        // Определяем границы видимости камеры в мировых координатах
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Рассчитываем размеры видимой области
        float screenWidth = screenTopRight.x - screenBottomLeft.x;
        float screenHeight = screenTopRight.y - screenBottomLeft.y;

        // Определяем расстояние для спавна за границами камеры, в диапазоне от minSpawnDistance до maxSpawnDistance
        float spawnDistanceX = Random.Range(screenWidth / 2 + minSpawnDistance, screenWidth / 2 + maxSpawnDistance);
        float spawnDistanceY = Random.Range(screenHeight / 2 + minSpawnDistance, screenHeight / 2 + maxSpawnDistance);

        // Выбираем случайную сторону для спавна
        if (Random.value > 0.5f) spawnDistanceX = -spawnDistanceX;
        if (Random.value > 0.5f) spawnDistanceY = -spawnDistanceY;

        // Возвращаем позицию спавна за пределами экрана в указанном диапазоне
        return new Vector3(playerPosition.x + spawnDistanceX, playerPosition.y + spawnDistanceY, playerPosition.z);
    }

    // Удаляем врагов из списка при их уничтожении
    public void RemoveEnemy(GameObject enemy)
    {
        if (spawnedEnemies.Contains(enemy))
        {
            Debug.Log($"Удаление врага {enemy.name} из списка спавнера");
            spawnedEnemies.Remove(enemy);
        }
        else
        {
            Debug.LogWarning($"Попытка удалить врага {enemy.name}, которого нет в списке");
        }
    }

}
