using UnityEngine;

public class Bomber : Tower
{
    private int _cost = 60;

    [Header("투석기탄 모델")]
    [SerializeField] GameObject _parabola;

    [Header("폭탄 범위")]
    [SerializeField] float _boomRange;

    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

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

    public override string TowerName => "Bomber";

    public override bool Upgrade()
    {
        if (_isUpgraded)
            return false;

        _isUpgraded = true;
        _damage *= 2;
        _boomRange *= 1.5f; // 폭탄 범위 증가
        return _isUpgraded;
    }

    protected override void Attack()
    {
        if (_currentTarget == null)
            return;

        Look();
        if (_projectile != null)
        {
            var parabola = Instantiate(_projectile, _firePoint.position, _firePoint.rotation, transform);
            parabola.GetComponent<Parabola>().Initialize(_currentTarget.position, _damage, _boomRange);

            AudioManager.Instance.PlaySoundEffect("Bomber");
            _animator.SetTrigger("Attack");
        }

        if (_attackEffect != null)
        {
            Instantiate(_attackEffect, _firePoint.position, _firePoint.rotation, transform);
        }
    }

    public override bool PossibleToTower()
    {
        int totalMoney = GameManager.Instance.MoneyManager.Money;

        return (totalMoney >= _cost);
    }

    public override void PlaceTower()
    {
        GameManager.Instance.MoneyManager.SpendMoney("Bomber");
    }

    private void Hide()
    {
        _parabola.SetActive(false);
    }

    private void Show()
    {
        _parabola.SetActive(true);
    }
}