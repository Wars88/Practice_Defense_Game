using UnityEngine;

public class Bomber : Tower
{
    private int _cost = 50;

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

    protected override void Attack()
    {
        if (_currentTarget == null)
            return;

        Look();
        if (_projectile != null)
        {
            var parabola = Instantiate(_projectile, _firePoint.position, _firePoint.rotation, transform);
            parabola.GetComponent<Parabola>().Initialize(_currentTarget.position, _damage, _boomRange);

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