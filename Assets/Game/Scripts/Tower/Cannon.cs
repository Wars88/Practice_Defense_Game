using UnityEngine;

public class Cannon : Tower
{
    private int _cost = 30;

    private void Update()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer >= _attackCooldown)
        {
            _attackTimer = 0f;
            CheckEnemy();
            TrackTarget();
            Attack();
        }

    }

    public override int Cost => _cost;

    public override bool Upgrade()
    {
        throw new System.NotImplementedException();
    }

    public override bool PossibleToTower()
    {
        int totalMoney = GameManager.Instance.MoneyManager.Money;

        return (totalMoney >= _cost);
    }

    public override void PlaceTower()
    {
        GameManager.Instance.MoneyManager.SpendMoney("Cannon");
    }
}