using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _target;
    private float _speed;
    private int _damage;

    public void Initialize(Transform target, float speed, int damage)
    {
        _target = target;
        _speed = speed;
        _damage = damage;
    }

    private void Update()
    {
        transform.LookAt(_target);
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                AffectDamage(enemy);
                Destroy(gameObject);
            }

        }
    }

    private void AffectDamage(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
    }

}