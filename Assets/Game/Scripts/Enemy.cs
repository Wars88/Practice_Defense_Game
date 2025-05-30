using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform[] _waypoints;
    private int _currentWaypointIndex = 0;
    private bool _isArrived = false;
    
    private float _speed;
    private int _health;
    private int _damage;

    private void Update()
    {
        Move();
    }

    public void Initialize(float speed, int health, int damage)
    {
        Path path = FindObjectOfType<Path>();

        if (path != null)
            _waypoints = path.Waypoints;

        _speed = speed;
        _health = health;
        _damage = damage;
    }

    private void Move()
    {
        if (_currentWaypointIndex == _waypoints.Length)
        {
            _isArrived = true;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, _speed * Time.deltaTime);
        transform.LookAt(_waypoints[_currentWaypointIndex].position);

        if (Vector3.Distance(transform.position, _waypoints[_currentWaypointIndex].position) < 0.1f)
            _currentWaypointIndex++;
    }
}
