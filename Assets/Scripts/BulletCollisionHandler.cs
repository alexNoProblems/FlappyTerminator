using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class BulletCollisionHandler : MonoBehaviour
{
    [SerializeField] private bool _isEnemyBullet;

    private Bullet _bullet;

    private void Awake()
    {
        _bullet = GetComponent<Bullet>();
    }

    private void OnEnable()
    {
        _bullet.Hit += OnTriggerEnter2D;
    }

    private void OnDisable()
    {
        _bullet.Hit -= OnTriggerEnter2D;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isEnemyBullet)
        {   
            var baloo = collision.GetComponentInParent<Baloo>();
            if (baloo != null)
                baloo.TriggerGameOver();
        }
        else
        {
            var enemy = collision.GetComponentInParent<Enemy>();
            if (enemy != null)
                enemy.Kill();
        }

        _bullet.ReturnToPool();
    }
}
