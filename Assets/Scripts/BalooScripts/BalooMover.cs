using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BalooMover : MonoBehaviour
{
    [SerializeField] private float _tapForce = 5f;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _maxRotationZ = 30f;
    [SerializeField] private float _minRotationZ = -90f;

    private Rigidbody2D _rigidBody2D;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    public void MoveUp()
    {
        _rigidBody2D.velocity = new Vector2(_speed, _tapForce);
        transform.rotation = _maxRotation;
    }

    public void RotateDown()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }
}
