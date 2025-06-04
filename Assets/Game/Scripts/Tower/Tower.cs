using System.Collections.Generic;
using UnityEngine;

public abstract class  Tower : MonoBehaviour
{
    [SerializeField] float _attackRange = 5f;
    [SerializeField] protected float _attackCooldown = 1f;
    [SerializeField] int _damage = 10;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] Transform _currentTarget;

    public List<Collider> _enemiesInRange = new List<Collider>();
    protected float _attackTimer = 0f;

    [SerializeField] GameObject _projectile;
    [SerializeField] GameObject _attackEffect;
    [SerializeField] float __projectileSpeed;
    private Transform _weapon;
    private Transform _firePoint;

    private void Awake()
    {
        Initialize();
    }


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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }

    protected virtual void Attack()
    {
        if (_currentTarget == null)
            return;

        Look();
        
        var projectile = Instantiate(_projectile, _firePoint.position, _firePoint.rotation, transform);
        Instantiate(_attackEffect, _firePoint.position, _firePoint.rotation, transform);
        projectile.GetComponent<Projectile>().Initialize(_currentTarget, __projectileSpeed, _damage);
    }

    private void Look()
    {
        if (_currentTarget != null)
            _weapon.rotation = Quaternion.Euler(_weapon.rotation.x, Quaternion.LookRotation(_currentTarget.position - _weapon.position).eulerAngles.y, _weapon.rotation.z);
    }
}