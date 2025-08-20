using System;

public interface IPoolable<T>
{
    void Init(Action<T> releaseToPool);
}
