using UnityEngine;
using UnityEngine.Pool;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private EnemyBulletPool _pool;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _cooldown = 1f;

    private float _nextShootTime;
    private bool _isShooting;

    private void Update()
    {
        if (!_isShooting)
            return;
        
        if (Time.time >= _nextShootTime)
        {
            _nextShootTime = Time.time + _cooldown;
            Vector2 direction = -transform.right;

            Bullet bullet = _pool.Get();
            bullet.IgnoreCollisionWith(gameObject);
            bullet.Fire(_bulletSpawnPoint.position, direction);
        }
    }

    private void OnDisable()
    {
        _isShooting = false;
    }

    public void StartShooting()
    {
        _isShooting = true;
        _nextShootTime = Time.time + _cooldown;
    }

    public void StopShooting()
    {
        _isShooting = false;
    }
}
