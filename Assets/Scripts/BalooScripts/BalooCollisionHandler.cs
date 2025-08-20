using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BalooCollisionHandler : MonoBehaviour
{
    private Collider2D _trigger;
    
    public event Action<IInteractable> CollisionDetected;

    private void Awake()
    {
        _trigger = GetComponent<Collider2D>();
        _trigger.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactible))
        {
            CollisionDetected?.Invoke(interactible);
        }
    }
}
