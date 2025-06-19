using UnityEngine;
using TMPro;

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

    public GameObject StageEndPannel;
    public Star ClearStar;
    public TMP_Text ClearText;
    public TMP_Text DefeatText;
    public Button TryAgain;
    public Button Next;

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
        GameManager.Instance.onEnemyCountChange += UpdateEnemyCount;
        SlowButton.OnClick += OnTowerButtonClicked;
        Bomber.OnClick += OnTowerButtonClicked;

        UpdateEnemyCount();
    }

    public void OnTowerButtonClicked(GameObject tower)
    {
        _towerManager.TowerPlacement(tower);
    }

    public void OnTryAgainClicked()
    {
        Debug.Log("Try Again Clicked");
    }

    public void OnNextClicked()
    {
        Debug.Log("Next Clicked");
    }

    public void ShowClearPannel()
    {
        StageEndPannel.SetActive(true);
        ClearStar.SetStar(_castle.CurrentHealth % 10);
        DefeatText.gameObject.SetActive(false);
    }

    public void ShowDefeatPannel()
    {
        StageEndPannel.SetActive(true);
        ClearStar.gameObject.SetActive(false);
        ClearText.gameObject.SetActive(false);
        Next.gameObject.SetActive(false);
    }

    public void UpdateEnemyCount()
    {
        EnemyCountText.text = GameManager.Instance.NextEnemyCount.ToString();
    }
}