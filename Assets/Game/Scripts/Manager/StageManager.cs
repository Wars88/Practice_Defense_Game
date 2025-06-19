using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    private EnemySpawner _enemySpawner;
    public int CurrentStageIndex = 0; // 현재 스테이지 인덱스

    public StageData[] StageDatas; // 웨이브 데이터 배열

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        _enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += Init;
    }

    private void Init(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f; // 게임 속도 초기화

        StartCoroutine(WaitAndInitSpawner());
    }

    private IEnumerator WaitAndInitSpawner()
    {
        yield return null; // 한 프레임 대기

        _enemySpawner = FindAnyObjectByType<EnemySpawner>();

        if (_enemySpawner != null)
        {
            Debug.Log($"{SceneManager.GetActiveScene().name} 씬에서 EnemySpawner 발견");
            _enemySpawner.StageInit(StageDatas[CurrentStageIndex]);
            _enemySpawner.StartSpawn();
        }
        else
        {
            Debug.LogWarning("EnemySpawner를 찾지 못했습니다.");
        }
    }
}