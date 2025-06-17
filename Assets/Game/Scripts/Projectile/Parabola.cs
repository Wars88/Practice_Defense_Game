using UnityEngine;

public class Parabola : MonoBehaviour
{
    [Header("발사 설정")]
    public float _launchForce = 10f;
    public float _upwardForce = 1f;

    private int _damage;
    private float _range;

    private Rigidbody _rb;

    public void Initialize(Vector3 targetPos, int damage, float range)
    {
        _rb = GetComponent<Rigidbody>();

        Vector3 dir = (targetPos - transform.position).normalized;

        Vector3 launchVelocity = dir * _launchForce + Vector3.up * _upwardForce;
        _rb.velocity = launchVelocity;

        _damage = damage;
        _range = range;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Placement")
            || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            CheckRange();
            Destroy(gameObject);
        }
    }

    private void CheckRange()
    {
        int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");

        Collider[] _enemiesInRange = Physics.OverlapSphere(transform.position, _range, enemyLayerMask);

        foreach (Collider col in _enemiesInRange)
        {
            var enemy = col.GetComponent<Enemy>();

            if (enemy != null)
                enemy.TakeDamage(_damage);
        }
    }

}