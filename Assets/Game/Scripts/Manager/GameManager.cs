using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public MoneyManager MoneyManager { get; private set; }
    public TowerManager TowerManager { get; private set; }
    [SerializeField] private Castle _castle;

    public UnityAction onEnemyCountChange;
    public UnityAction OnEnemyDead;
    public UnityEvent OnGameOver;

    public int NextEnemyCount = 15;
    public float WaveTime;
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
        TowerManager = FindAnyObjectByType<TowerManager>();
    }

    private void Start()
    {
        OnEnemyDead += EnemyClear;
        OnGameOver.AddListener(GameOver);
    }

    private void OnDestroy()
    {
        OnEnemyDead -= EnemyClear;
        OnGameOver.RemoveListener(GameOver);
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

    private void EnemyClear()
    {
        if (RemainingEnemyCount <= 0)
        {
            IsEnemyClear = true;
            StartCoroutine(StageClear());
        }
        else
            IsEnemyClear = false;
    }

    private IEnumerator StageClear()
    {
        if (_castle.CurrentHealth <= 0)
            yield break;

        if (RemainingEnemyCount <= 0)
        {
            if (IsStageDone && IsEnemyClear)
            {
                yield return null;
                GUIManager.Instance.ShowClearPannel();

                Time.timeScale = 0f; // 게임 일시정지
            }
        }

    }

    private void GameOver()
    {
        if (IsGameOver)
        {
            GUIManager.Instance.ShowDefeatPannel();

            Time.timeScale = 0f; // 게임 일시정지
        }
    }

    public IEnumerator Timeroutine(float delayTime)
    {
        WaveTime = delayTime;

        while(WaveTime > 0f)
        {
            WaveTime -= Time.deltaTime;
            GUIManager.Instance.UpdateTimeText(WaveTime);
            yield return null;
        }

        WaveTime = 0f;
    }
}
