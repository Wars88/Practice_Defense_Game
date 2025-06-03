using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class  EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] _enemisData;

    private Path _path;

    private Transform _spawnPoint;
    private int _currentEnemyCount = 0;
    private int _currentEnemyIndex = 0;

    private float _spawnTime = 1.5f;

    private void Awake()
    {
        _spawnPoint = transform.GetChild(0);
        _path = FindObjectOfType<Path>();
    }

    private void Start()
    {
        Spawn();
    }

    private IEnumerator SpawnEnemies(float spawnCoolTime)
    {
        EnemyData currentEnemyData = _enemisData[_currentEnemyIndex];

        for (_currentEnemyCount = 0; _currentEnemyCount < currentEnemyData.SpawnCount; _currentEnemyCount++)
        {
            yield return new WaitForSeconds(spawnCoolTime);

            GameObject enemy = Instantiate(currentEnemyData.Prefab,
                _spawnPoint.position, Quaternion.identity, this.transform);

            var enemyClass = enemy.GetComponent<Enemy>();

            if (enemyClass != null)
            {
                enemyClass.Initialize(currentEnemyData.Speed, currentEnemyData.MaxHealth,
                    currentEnemyData.Damage, _path.Waypoints);
            }
        }

        _currentEnemyCount = 0;
    }

    // 할당과 함께 소환
    private void Spawn()
    {
        StartCoroutine(SpawnEnemies(_spawnTime));
    }
}
