using UnityEngine;

[RequireComponent(typeof(Baloo), typeof(SceneReloader))]
public class BalooGroundCollisionHandler : MonoBehaviour
{
    private Baloo _baloo;

    private void Awake()
    {
        _baloo = GetComponent<Baloo>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground ground))
            _baloo.TriggerGameOver();
    }
}
