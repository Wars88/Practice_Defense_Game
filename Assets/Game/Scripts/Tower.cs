using System.Collections.Generic;
using UnityEngine;

public abstract class  Tower : MonoBehaviour
{
    [SerializeField] float _attackRange = 5f;
    [SerializeField] protected float _attackCooldown = 1f;
    [SerializeField] int _damage = 10;
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] Collider _currentTarget;

    public List<Collider> _enemiesInRange = new List<Collider>();
    protected float _attackTimer = 0f;

    public void CheckEnemy()
    {
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
        foreach (Collider enemy in _enemiesInRange)
        {

            if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, _currentTarget.transform.position))
            {
                _currentTarget = enemy;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}