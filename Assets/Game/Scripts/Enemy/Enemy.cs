using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform[] _waypoints;
    private int _currentWaypointIndex = 0;
    private bool _isArrived = false;
    private Slider _healthBarSlider;

    private float _speed;
    private int _maxHealth;
    private int _health;
    private int _damage;

    private void Awake()
    {
        _healthBarSlider = GetComponentInChildren<Slider>(true);
    }

    private void Update()
    {
        Move();
        VidwHealthBar();
    }

    public void Initialize(float speed, int maxHealth, int damage, Transform[] waypoints)
    {
        _speed = speed;
        _maxHealth = maxHealth;
        _health = maxHealth;
        _damage = damage;
        _waypoints = waypoints;
    }

    private void VidwHealthBar()
    {
        _healthBarSlider.transform.rotation = Camera.main.transform.rotation;
        _healthBarSlider.value = (float)_health / _maxHealth;
    }

    private void Move()
    {
        if (_isArrived)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, _speed * Time.deltaTime);
        transform.LookAt(_waypoints[_currentWaypointIndex].position);

        if (Vector3.Distance(transform.position, _waypoints[_currentWaypointIndex].position) < 0.1f)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex == _waypoints.Length)
                _isArrived = true;
        }
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);
        _healthBarSlider.gameObject.SetActive(true);

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
