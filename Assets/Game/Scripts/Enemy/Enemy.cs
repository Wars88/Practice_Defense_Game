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
    private int _deadMoney;
    private bool _isDead;

    private void Awake()
    {
        _healthBarSlider = GetComponentInChildren<Slider>(true);
    }



    private void Update()
    {
        Move();
        VidwHealthBar();
    }

    public void Initialize(float speed, int maxHealth, Transform[] waypoints, int deadMoney)
    {
        _speed = speed;
        _maxHealth = maxHealth;
        _health = maxHealth;
        _waypoints = waypoints;
        _deadMoney = deadMoney;
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
        AudioManager.Instance.PlaySoundEffect("EnemyHit");

        if (_health <= 0)
            Die();
    }

    private void Die()
    {
        if (_isDead) return; // 이미 죽은 경우 중복 처리 방지

        _isDead = true;
        GameManager.Instance.MoneyManager.GetMoney(_deadMoney);
        GameManager.Instance.RemainingEnemyCount--;
        GameManager.Instance.OnEnemyDead?.Invoke();

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Castle"))
        {
            Castle castle = other.GetComponent<Castle>();
            if (castle != null)
            {
                castle.TakeDamage();
                GameManager.Instance.RemainingEnemyCount--;
                GameManager.Instance.OnEnemyDead?.Invoke();

                Destroy(gameObject);
            }
        }
    }

    public void SlowDown(float duration)
    {
        StartCoroutine(SlowDownCoroutine(duration));
    }

    private IEnumerator SlowDownCoroutine(float duration)
    {
        float originalSpeed = _speed;
        _speed /= 2; // Slow down to half speed
        yield return new WaitForSeconds(duration);
        _speed = originalSpeed; // Restore original speed
    }
}
