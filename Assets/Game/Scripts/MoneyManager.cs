using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public int Money { get; private set; }
    private Dictionary<string, int> TowerCost;
    private void Awake()
    {
        TowerCost = new Dictionary<string, int>
        {
            {"Cannon", 100}

        };
    }
    public void GetMoney(int money)
    {
        Money += money;
    }

    public void SpendMoney(int money)
    {
        Money -= money;
    }
}