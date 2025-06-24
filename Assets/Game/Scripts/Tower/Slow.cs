using UnityEngine;

public class Slow : Tower
{
    private int _cost = 40;
    private float _stateTime = 1.5f;

    private void Update()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer >= _attackCooldown)
        {
            _attackTimer = 0f;
            CheckEnemy();
            Attack();
        }

    }

    public override int Cost => _cost;

    public override string TowerName => "Slow";
    public override bool Upgrade()
    {
        if (_isUpgraded)
            return false;

        _isUpgraded = true;
        _stateTime *= 2;

        return _isUpgraded;
    }

    protected override void Attack()
    {
        if (_enemiesInRange.Count == 0)
            return;

        AudioManager.Instance.PlaySoundEffect("Slow");
        foreach (var enemy in _enemiesInRange)
        {
            if (enemy != null) // null 체크 추가
            {
                var enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.SlowDown(_stateTime);
                }
            }
        }
        
    }

    public override bool PossibleToTower()
    {
        int totalMoney = GameManager.Instance.MoneyManager.Money;

        return (totalMoney >= _cost);
    }

    public override void PlaceTower()
    {
        GameManager.Instance.MoneyManager.SpendMoney("Slow");
    }
}