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
    public Button Sell;
    public Button Close;

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
        Debug.Log("Upgrade clicked");
    }

    public void OnSellClicked()
    {
        _towerManager.TowerDelete();
    }

}