using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyProperties> enemyTypePropertiesList = new List<EnemyProperties>();

    private Dictionary<EnemyType, EnemyProperties> enemyPropertiesDict;

    private void Awake()
    {
        enemyPropertiesDict = new Dictionary<EnemyType, EnemyProperties>();
        foreach (var properties in enemyTypePropertiesList)
        {
            enemyPropertiesDict.Add(properties.enemyType, properties);
        }
    }

    public GameObject GetEnemyPrefab(EnemyType enemyType)
    {
        if (enemyPropertiesDict.TryGetValue(enemyType, out EnemyProperties properties))
        {
            return properties.enemyPrefab;
        }
        else
        {
            Debug.LogWarning($"EnemyType not found: {enemyType}");
            return null;
        }
    }

    public float GetSpawnRate(EnemyType enemyType)
    {
        if (enemyPropertiesDict.TryGetValue(enemyType, out EnemyProperties properties))
        {
            return properties.baseSpawnRate;
        }
        else
        {
            Debug.LogWarning($"Spawn rate not found for EnemyType: {enemyType}");
            return 0f;
        }
    }
}
