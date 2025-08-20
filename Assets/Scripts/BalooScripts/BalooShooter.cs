using System;
using UnityEngine;

public class BalooShooter : MonoBehaviour
{
    [SerializeField] private BalooBulletPool _pool;
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _cooldown = 0.2f;

    private float _nextShootTime;

    public event Action ShootFired;

    public void TryShoot()
    {
        if (Time.time < _nextShootTime || _pool == null || _bulletSpawnPoint == null)
            return;

        _nextShootTime = Time.time + _cooldown;
        Vector2 direction = transform.right;

        Bullet bullet = _pool.Get();
        bullet.IgnoreCollisionWith(gameObject);
        bullet.Fire(_bulletSpawnPoint.position, direction);

        ShootFired?.Invoke();
    }
}
