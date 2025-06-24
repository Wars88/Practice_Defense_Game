using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] StageData _stageData; // 스테이지 데이터
    [SerializeField] WaveData _currentWaveData; // 현재 웨이브 데이터   

    private Path _path;

    private Transform _spawnPoint;

    public int _currentWaveIndex = 0; // 현재 웨이브 인덱스
    private float _waveDelay = 2.5f; // 웨이브간 딜레이

    private void Awake()
    {
        _spawnPoint = transform.GetChild(0);
        _path = FindObjectOfType<Path>();
    }

    public void StageInit(StageData stageData)
    {
        _stageData = stageData;
        _currentWaveIndex = 0; // 웨이브 인덱스 초기화
        GameManager.Instance.IsStageDone = false; // 스테이지 완료 상태 초기화
    
        foreach(var waveData in _stageData.WaveData)
        {
            foreach (var spawnInfo in waveData.EnemiesToSpawn)
            {
                GameManager.Instance.RemainingEnemyCount += spawnInfo.SpawnCount;
            }
        }
        Debug.Log($"Remaining Enemy Count: {GameManager.Instance.RemainingEnemyCount}");
    }

    private void WaveInit()
    {
        _currentWaveData = _stageData.WaveData[_currentWaveIndex];
        _waveDelay = _currentWaveData.WaveStartDelay;
    }

    public void StartSpawn()
    {
        StartCoroutine(Waveroutine());
    }

    private IEnumerator Waveroutine()
    {
        WaveInit();

        // 웨이브 몬스터 수 UI 업데이트
        int totalEnemyCount = 0;

        foreach (var spawnInfo in _currentWaveData.EnemiesToSpawn)
            totalEnemyCount += spawnInfo.SpawnCount;

        GameManager.Instance.EnemyCountReset(totalEnemyCount);

        // 웨이브 시작 전 딜레이
        StartCoroutine(GameManager.Instance.Timeroutine(_waveDelay));
        yield return new WaitForSeconds(_waveDelay);
        StartCoroutine(Spawnroutine());
    }

    private IEnumerator Spawnroutine()
    {
        foreach (var spawnInfo in _currentWaveData.EnemiesToSpawn)
        {
            for (int enemyCount = 0; enemyCount < spawnInfo.SpawnCount; enemyCount++)
            {
                // 몬스터 수 UI 업데이트
                GameManager.Instance.EnemyCountDown();

                var enemyData = spawnInfo.EnemyData;
                var enemy = Instantiate(enemyData.Prefab, _spawnPoint.position, Quaternion.identity, this.transform);

                if (enemy != null)
                    enemy.GetComponent<Enemy>().Initialize(enemyData.Speed, enemyData.MaxHealth, _path.Waypoints, enemyData.DeadMoney);

                AudioManager.Instance.PlaySoundEffect("EnemySpawn");
                yield return new WaitForSeconds(spawnInfo.SpawnDelay);
            }
        }

        _currentWaveIndex++; // 다음 웨이브로 이동
        if (_currentWaveIndex == _stageData.WaveData.Length)
        {
            GameManager.Instance.IsStageDone = true;
            yield break;
        }
        
        StartCoroutine(Waveroutine());
    }
}
