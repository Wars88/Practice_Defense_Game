using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public MoneyManager MoneyManager { get; private set; }
    private EnemySpawner _enemySpawner;

    public UnityAction OnEnemyClear;
    public UnityAction onEnemyCountChange;

    public int EnemyCount = 15;
    public bool IsStageDone { get; set; } = false;
    public int EnemyInMap { get; set; } = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        MoneyManager = FindAnyObjectByType<MoneyManager>();
        _enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    private void Start()
    {
        OnEnemyClear += StageClear;
        _enemySpawner.Spawn();
    }

    private void OnDestroy()
    {
        OnEnemyClear -= StageClear;
    }


    public void WaveSpawn()
    {
        StartCoroutine(WaveSpawnCoroutine(7f));
    }

    private IEnumerator WaveSpawnCoroutine(float spawnCoolTime)
    {
        yield return new WaitForSeconds(spawnCoolTime);
        _enemySpawner.Spawn();
    }

    public void EnemyCountReset(int count)
    {
        EnemyCount = count;
        onEnemyCountChange?.Invoke();
    }

    public void EnemyCountDown()
    {
        EnemyCount--;
        onEnemyCountChange?.Invoke();
    }

    private void StageClear()
    {
        OnEnemyClear?.Invoke();
        if (IsStageDone)
        {
            Debug.Log("스테이지 클리어!");
        }
    }
}
