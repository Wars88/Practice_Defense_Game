using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public abstract class  Tower : MonoBehaviour
{
    [SerializeField] float _attackRange = 5f;
    [SerializeField] protected float _attackCooldown = 1f;
    [SerializeField] protected int _damage = 10;
    [SerializeField] protected LayerMask _enemyLayer;
    [SerializeField] protected Transform _currentTarget;
    protected bool _isUpgraded = false;

    public bool IsClicked = false;

    public List<Collider> _enemiesInRange = new List<Collider>();
    protected float _attackTimer = 0f;

    [SerializeField] protected GameObject _projectile;
    [SerializeField] protected GameObject _attackEffect;
    [SerializeField] protected GameObject _rangeEffect;
    [SerializeField] float __projectileSpeed;
    protected Transform _weapon;
    protected Transform _firePoint;

    protected virtual void Awake()
    {
        Initialize();
    }

    public abstract int Cost { get; }
    public abstract bool Upgrade();
    protected virtual void Initialize()
    {
        _weapon = transform.GetChild(1);
        _firePoint = _weapon.GetChild(1);
    }

    public void CheckEnemy()
    {
        // Physics.OverlapSphere사용
        Collider[] enemiesInArray = Physics.OverlapSphere(transform.position, _attackRange, _enemyLayer);

        _enemiesInRange.Clear();

        foreach (Collider enemy in enemiesInArray)
        {
            if (!_enemiesInRange.Contains(enemy))
            {
                _enemiesInRange.Add(enemy);
            }
        }
    }

    public void TrackTarget()
    {
        if (_enemiesInRange.Count == 0)
        {
            _currentTarget = null;
            return;
        }

        foreach (Collider enemy in _enemiesInRange)
        {
            if (_currentTarget == null)
                _currentTarget = enemy.transform;
            else
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, _currentTarget.transform.position))
                {
                    _currentTarget = enemy.transform;
                }
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, _attackRange);
    //}

    protected virtual void Attack()
    {
        if (_currentTarget == null)
            return;

        Look();
        if (_projectile != null)
        {
            var projectile = Instantiate(_projectile, _firePoint.position, _firePoint.rotation, transform);
            projectile.GetComponent<Projectile>().Initialize(_currentTarget, __projectileSpeed, _damage);
        }

        if (_attackEffect != null)
        {
            Instantiate(_attackEffect, _firePoint.position, _firePoint.rotation, transform);

        }
    }

    protected void Look()
    {
        if (_currentTarget != null)
            _weapon.rotation = Quaternion.Euler(_weapon.rotation.x, Quaternion.LookRotation(_currentTarget.position - _weapon.position).eulerAngles.y, _weapon.rotation.z);
    }

    private void OnMouseDown()
    {
        GameManager.Instance.TowerManager.TowerSelect(transform.position);
    }

    public abstract bool PossibleToTower();
    public abstract void PlaceTower();

}