using System.Collections;
using UnityEngine;

public class  EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyData[] _enemisData;

    private Path _path;

    private Transform _spawnPoint;
    private int _currentEnemyCount = 0;
    private int _currentEnemyIndex = 0;
    private float _spawnTime = 2.5f;

    private void Awake()
    {
        _spawnPoint = transform.GetChild(0);
        _path = FindObjectOfType<Path>();
    }

    private IEnumerator SpawnEnemies(float spawnCoolTime)
    {
        EnemyData currentEnemyData = _enemisData[_currentEnemyIndex];
        
        for (_currentEnemyCount = 0; _currentEnemyCount < currentEnemyData.SpawnCount; _currentEnemyCount++)
        {
            yield return new WaitForSeconds(spawnCoolTime);

            GameManager.Instance.EnemyCount--;

            GameObject enemy = Instantiate(currentEnemyData.Prefab,
                _spawnPoint.position, Quaternion.identity, this.transform);

            

            var enemyClass = enemy.GetComponent<Enemy>();

            if (enemyClass != null)
            {
                enemyClass.Initialize(currentEnemyData.Speed, currentEnemyData.MaxHealth, _path.Waypoints, currentEnemyData.DeadMoney);
            }

        }

        if (_currentEnemyIndex == _enemisData.Length - 1)
        {
            GameManager.Instance.IsStageDone = true;
            GameManager.Instance.EnemyCount = 0;
            yield break;
        }

        yield return new WaitForSeconds(2);

        _currentEnemyIndex++;
        GameManager.Instance.EnemyCount = _enemisData[_currentEnemyIndex].SpawnCount;

        GameManager.Instance.WaveSpawn();
    }

    // 할당과 함께 소환
    public void Spawn()
    {
        _spawnTime = _enemisData[_currentEnemyIndex].SpawnTime;

        StartCoroutine(SpawnEnemies(_spawnTime));
    }


}
