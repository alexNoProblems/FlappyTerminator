using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletPoolBase<T> : MonoBehaviour where T : MonoBehaviour, IPoolable<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _capacity = 30;
    [SerializeField] private Transform _bulletContainer;

    private readonly Queue<T> _pool = new Queue<T>();

    protected virtual void Awake()
    {
        for (int i = 0; i < _capacity; i++)
        {
            var item = Instantiate(_prefab, _bulletContainer);
            item.gameObject.SetActive(false);
            item.Init(Return);
            _pool.Enqueue(item);
        }
    }

    public T Get()
    {
        T item = _pool.Count > 0 ? _pool.Dequeue() : ForceReuse();
        item.gameObject.SetActive(true);

        return item;
    }

    public void Return(T item)
    {
        item.gameObject.SetActive(false);

        if (_bulletContainer != null)
            item.transform.SetParent(_bulletContainer, false);

        _pool.Enqueue(item);
    }

    private T ForceReuse()
    {
        T reused = _pool.Dequeue();
        reused.gameObject.SetActive(false);

        return reused;
    }
}
