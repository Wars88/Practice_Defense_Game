using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    private TowerManager _towerManager;
    private Castle _castle;

    public static GUIManager Instance { get; private set; }

    public TowerButton TowerButton;
    public SlowButton SlowButton; 
    public SlowButton Bomber;
    public TMP_Text MoneyText;
    public TMP_Text HpText;
    public TMP_Text EnemyCountText;
    public TMP_Text TimeText;

    public GameObject StageEndPannel;
    public Star ClearStar;
    public TMP_Text ClearText;
    public TMP_Text DefeatText;
    public Button TryAgain;
    public Button ToLevel;
    public Button Next;

    public GameObject UpgradePannel;
    public Button Upgrade;
    public TMP_Text UpgradeText;
    public Button Sell;
    public TMP_Text SellText;
    public Button Close;

    public Button PauseButton;
    public GameObject PasuePannel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _towerManager = FindAnyObjectByType<TowerManager>();
        _castle = FindAnyObjectByType<Castle>();
    }

    private void Start()
    {
        TryAgain.onClick += OnTryAgainClicked;
        Next.onClick += OnNextClicked;
        ToLevel.onClick += OnLevelClicked;
        GameManager.Instance.onEnemyCountChange += UpdateEnemyCount;
        SlowButton.OnClick += OnTowerButtonClicked;
        Bomber.OnClick += OnTowerButtonClicked;
        Upgrade.onClick += OnUpgradeClicked;
        Sell.onClick += OnSellClicked;
        Close.onClick += HideUpgradePannel;
        PauseButton.onClick += OnPauseButtonClicked;

        UpdateEnemyCount();
        StageEndPannel.SetActive(false);
    }

    private void OnDisable()
    {
        TryAgain.onClick -= OnTryAgainClicked;
        Next.onClick -= OnNextClicked;
        ToLevel.onClick -= OnLevelClicked;
        GameManager.Instance.onEnemyCountChange -= UpdateEnemyCount;
        SlowButton.OnClick -= OnTowerButtonClicked;
        Bomber.OnClick -= OnTowerButtonClicked;
        Upgrade.onClick -= OnUpgradeClicked;
        Sell.onClick -= OnSellClicked;
        Close.onClick -= HideUpgradePannel;
        PauseButton.onClick -= OnPauseButtonClicked;
    }

    public void OnPauseButtonClicked()
    {
        if (Time.timeScale == 1)
        {
            Debug.Log("Pause Game");
            Time.timeScale = 0; // 게임 일시정지
            PasuePannel.SetActive(true);
        }
        else
        {
            Debug.Log("Resume Game");
            Time.timeScale = 1; // 게임 일시정지
            PasuePannel.SetActive(false);
        }
    }

    public void OnTowerButtonClicked(GameObject tower)
    {
        _towerManager.TowerPlacement(tower);
    }

    public void OnTryAgainClicked()
    {
        SceneManager.LoadScene("Stage");
    }

    public void OnToLevelClicked()
    {
        SceneManager.LoadScene("Level Select");
    }
    public void OnNextClicked()
    {
        // 다음 스테이지인덱스
        if (StageManager.Instance.CurrentStageIndex < 3)
            StageManager.Instance.CurrentStageIndex++;

        SceneManager.LoadScene("Stage");
    }

    public void OnLevelClicked()
    {
        SceneManager.LoadScene("Level Select");
    }


    public void ShowClearPannel()
    {
        StageEndPannel.SetActive(true);
        ClearStar.gameObject.SetActive(true);
        ClearText.gameObject.SetActive(true);
        DefeatText.gameObject.SetActive(false);
        Next.gameObject.SetActive(true);
        ToLevel.gameObject.SetActive(true);
        TryAgain.gameObject.SetActive(true);
        ClearStar.SetStar(_castle.CurrentHealth / 3);
    }

    public void ShowDefeatPannel()
    {
        StageEndPannel.SetActive(true);
        ClearStar.gameObject.SetActive(false);
        ClearText.gameObject.SetActive(false);
        DefeatText.gameObject.SetActive(true);
        Next.gameObject.SetActive(false);
        TryAgain.gameObject.SetActive(true);
        ToLevel.gameObject.SetActive(true);
    }

    public void UpdateEnemyCount()
    {
        EnemyCountText.text = GameManager.Instance.NextEnemyCount.ToString();
    }

    public void UpdateTimeText(float time)
    {
        TimeText.text = $"Next: {time:F0}s";
    }

    public void ShowUpgradePannel()
    {
        UpgradePannel.SetActive(true);
        Upgrade.gameObject.SetActive(true);
        Sell.gameObject.SetActive(true);
        Close.gameObject.SetActive(true);

    }

    public void HideUpgradePannel()
    {
        UpgradePannel.SetActive(false);
    }

    public void OnUpgradeClicked()
    {
        bool isUpgraded = _towerManager.CurrentTower.GetComponent<Tower>().IsUpgraded;
        _towerManager.TowerUpgrade();

        if (isUpgraded)
        {
            UpgradeText.text = "UP Done";
            return;
        }


    }

    public void OnSellClicked()
    {
        _towerManager.TowerDelete();
        HideUpgradePannel();
    }

}