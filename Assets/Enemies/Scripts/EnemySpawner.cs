using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public EnemyType[] enemyTypes;

    private bool isSpawning = false;
    private EnemyManager enemyManager;
    private PlayAreaClamp playAreaClamp;

    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        playAreaClamp = FindObjectOfType<PlayAreaClamp>();
        StartSpawner();
    }

    public void StartSpawner()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            foreach (EnemyType enemyType in enemyTypes)
            {
                float spawnInterval = enemyManager.GetSpawnRate(enemyType);
                StartCoroutine(SpawnEnemies(enemyType, spawnInterval));
            }
        }
    }

    public void StopSpawner()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemies(EnemyType enemyType, float spawnInterval)
    {
        while (isSpawning)
        {
            SpawnEnemy(enemyType);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(EnemyType enemyType)
    {
        SpawnDirection randomDirection = (SpawnDirection)Random.Range(0, System.Enum.GetValues(typeof(SpawnDirection)).Length);
        Vector2 spawnLocation = GetRandomSpawnLocation(randomDirection);

        GameObject enemyPrefab = enemyManager.GetEnemyPrefab(enemyType);
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"Prefab not found for enemy type: {enemyType}");
        }
    }

    private Vector2 GetRandomSpawnLocation(SpawnDirection direction)
    {
        Vector2 clampMin = playAreaClamp.minClamp;
        Vector2 clampMax = playAreaClamp.maxClamp;

        float spawnX = Random.Range(clampMin.x, clampMax.x);
        float spawnY = Random.Range(clampMin.y, clampMax.y);

        switch (direction)
        {
            case SpawnDirection.Left:
                spawnX = clampMin.x - 5f;
                break;
            case SpawnDirection.Right:
                spawnX = clampMax.x + 5f;
                break;
            case SpawnDirection.Top:
                spawnY = clampMax.y + 5f;
                break;
            case SpawnDirection.Bottom:
                spawnY = clampMin.y - 5f;
                break;
        }

        return new Vector2(spawnX, spawnY);
    }
}
