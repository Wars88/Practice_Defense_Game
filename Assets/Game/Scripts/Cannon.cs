using UnityEngine;

public class Cannon : Tower
{

    private void Update()
    {
        _attackTimer += Time.deltaTime;

        if (_attackTimer >= _attackCooldown)
        {
            _attackTimer = 0f;
            Debug.Log("Cannon is ready to attack!");
            CheckEnemy();
            TrackTarget();
            Attack();
        }

    }

}