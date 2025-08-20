using UnityEngine;

public class BalooTracker : MonoBehaviour
{
    [SerializeField] private Baloo _baloo;
    [SerializeField] private float _xOffset;

    private void Update()
    {
        var position = transform.position;
        position.x = _baloo.transform.position.x + _xOffset;
        transform.position = position;
    }
}
