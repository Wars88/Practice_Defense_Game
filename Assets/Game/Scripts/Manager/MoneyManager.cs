using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int Money = 200;
    private Dictionary<string, int> TowerCost;
    private void Awake()
    {
        TowerCost = new Dictionary<string, int>
        {
            {"Cannon", 30},
            {"Slow", 40},
            {"Bomber", 60}
        };
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        switch(StageManager.Instance.CurrentStageIndex)
        {
            case 0:
                Money = 60;
                break;
            case 1:
                Money = 70;
                break;
            case 2:
                Money = 80;
                break;
            case 3:
                Money = 100;
                break;
        }
    }

    private void Update()
    {
        GUIManager.Instance.MoneyText.text 
            = $"Money: {Money.ToString()}"; // 업데이트에서 받는 경우
    }

    public void GetMoney(int money)
    {
        Money += money;
    }

    public void SpendMoney(string key)
    {
        if (TowerCost.TryGetValue(key, out int cost) && Money >= cost)
        {
            Money -= cost;
        }
        else
        {
            Debug.Log("이름 없거나 소비불가능");
        }
    }
}