using UnityEngine;

public class Slow : Tower
{
    private int _cost = 40;

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

    protected override void Attack()
    {
        foreach (var enemy in _enemiesInRange)
        {
            if (enemy != null) // null 체크 추가
            {
                var enemyComponent = enemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.SlowDown(1.5f);
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