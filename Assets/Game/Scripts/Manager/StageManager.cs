using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    private EnemySpawner _enemySpawner;
    public int CurrentStageIndex = 0; // ���� �������� �ε���

    public StageData[] StageDatas; // ���̺� ������ �迭

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
        Time.timeScale = 1f; // ���� �ӵ� �ʱ�ȭ

        StartCoroutine(WaitAndInitSpawner());
    }

    private IEnumerator WaitAndInitSpawner()
    {
        yield return null; // �� ������ ���

        _enemySpawner = FindAnyObjectByType<EnemySpawner>();

        if (_enemySpawner != null)
        {
            Debug.Log($"{SceneManager.GetActiveScene().name} ������ EnemySpawner �߰�");
            _enemySpawner.StageInit(StageDatas[CurrentStageIndex]);
            _enemySpawner.StartSpawn();
        }
        else
        {
            Debug.LogWarning("EnemySpawner�� ã�� ���߽��ϴ�.");
        }
    }
}