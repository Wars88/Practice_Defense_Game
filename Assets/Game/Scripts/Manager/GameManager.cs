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

    public int NextEnemyCount = 15;
    public int RemainingEnemyCount { get; set; } = 0;
    public bool IsStageDone { get; set; } = false;
    public bool IsGameOver { get; set; } = false;
    public bool IsEnemyClear { get; set; } = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        MoneyManager = FindAnyObjectByType<MoneyManager>();
        _enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    private void Start()
    {
        OnEnemyClear += StageClear;
    }

    private void OnDestroy()
    {
        OnEnemyClear -= StageClear;
    }

    public void EnemyCountReset(int count)
    {
        NextEnemyCount = count;
        onEnemyCountChange?.Invoke();
    }

    public void EnemyCountDown()
    {
        NextEnemyCount--;
        onEnemyCountChange?.Invoke();
    }

    private void StageClear()
    {
        if (IsStageDone && IsEnemyClear)
        {
            GUIManager.Instance.ShowClearPannel();
        }
    }

    private void GameOver()
    {
        if (IsGameOver)
        {
            GUIManager.Instance.ShowDefeatPannel();
        }
    }
}
