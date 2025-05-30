using Unity.VisualScripting;
using UnityEngine;

public class  EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] _enemisData;

    private Transform _spawnPoint;
    private int _currentEnemyCount = 0;
    private int _currentEnemyIndex = 0;

    private float _spawnTime = 1.5f;
    private float _timer = 0f;


    private void Awake()
    {
        _spawnPoint = transform.GetChild(0);
    }

    private void Start()
    {
        SpawnEnemies();
    }

    private void Initialize()
    {

        _enemisData[_currentEnemyIndex].Prefab
    }

    private void SpawnEnemies()
    {
        Instantiate(_enemisData[_currentEnemyIndex].Prefab, _spawnPoint.position, Quaternion.identity).

        foreach (var enemyData in _enemisData)
        {
            for (int i = 0; i < enemyData.SpawnCount; i++)
            {
                GameObject enemy = Instantiate(enemyData.Prefab, _spawnPoint.position, Quaternion.identity);
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                
                if (enemyComponent != null)
                {
                    enemyComponent.Speed = enemyData.Speed;
                    enemyComponent.Health = enemyData.Health;
                    enemyComponent.Damage = enemyData.Damage;
                }
            }
        }
    }
}
