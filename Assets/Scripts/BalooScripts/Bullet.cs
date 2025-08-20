using System;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour, IPoolable<Bullet>
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxLifetime = 3f;

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private float _lifeTimer;

    public Action<Collider2D> Hit;
    private Action<Bullet> _releaseToPool;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _lifeTimer = 0f;
    }

    private void Update()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer >= _maxLifetime)
            ReturnToPool();
    }

    public void Init(Action<Bullet> releaseToPool)
    {
        _releaseToPool = releaseToPool;
    }

    public void Fire(Vector2 position, Vector2 direction)
    {
        transform.position = position;

        if (_rigidbody2D != null)
        {
            _rigidbody2D.velocity = Vector2.zero;
            Vector2 impulse = direction.normalized * _speed;
            _rigidbody2D.AddForce(impulse, ForceMode2D.Impulse);
        }
    }

     public void ReturnToPool()
    {
        if (_rigidbody2D != null)
            _rigidbody2D.velocity = Vector2.zero;

        _releaseToPool?.Invoke(this);
    }

    public void IgnoreCollisionWith(GameObject owner)
    {
        if (_collider2D == null || owner == null)
            return;

        var ownerColliders = owner.GetComponentsInChildren<Collider2D>();

        foreach (var ownerCollider in ownerColliders)
        {
            if (ownerCollider != null)
                Physics2D.IgnoreCollision(_collider2D, ownerCollider, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == _collider2D)
            return;

        Hit?.Invoke(collision);
    }
}
