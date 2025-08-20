using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    const float HalfTile = 0.5f;

    [SerializeField] private Camera _camera;
    [SerializeField] Transform[] _tiles;

    private float _tileWidth;
    private float _halfCameraWidth;

    private void Awake()
    {
        if (_camera == null)
            _camera = Camera.main;

        if (_tiles == null || _tiles.Length == 0)
        {
            int childCount = transform.childCount;
            _tiles = new Transform[childCount];

            for (int i = 0; i < childCount; i++)
                _tiles[i] = transform.GetChild(i);
        }

        SpriteRenderer spriteRenderer = _tiles[0].GetComponent<SpriteRenderer>();
        _tileWidth = spriteRenderer.bounds.size.x;
        _halfCameraWidth = _camera.orthographicSize * _camera.aspect;
    }

    private void LateUpdate()
    {
        Transform leftMost = _tiles[0];
        Transform rightMost = _tiles[0];

        for (int i = 0; i < _tiles.Length; i++)
        {
            if (_tiles[i].position.x < leftMost.position.x)
                leftMost = _tiles[i];

            if (_tiles[i].position.x > rightMost.position.x)
                rightMost = _tiles[i];
        }

        float cameraLeft = _camera.transform.position.x - _halfCameraWidth;
        float leftTileRightEdge = leftMost.position.x + _tileWidth * HalfTile;

        if (cameraLeft >= leftTileRightEdge)
            leftMost.position = new Vector3(rightMost.position.x + _tileWidth, leftMost.position.y, leftMost.position.z);
    }
}
