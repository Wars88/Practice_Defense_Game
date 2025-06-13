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
            {"Cannon", 100},
            {"Ballista", 200},
            {"Slow", 100}

        };
    }

    private void Update()
    {
        GUIManager.Instance.MoneyText.text = $"Money: {Money.ToString()}";
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