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

    public override string TowerName => "Cannon";

    public override bool Upgrade()
    {
        if (_isUpgraded)
            return false;
        _isUpgraded = true;
        _attackCooldown *= 0.6f; // 공격 속도 증가
        return _isUpgraded;
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