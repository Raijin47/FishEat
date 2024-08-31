using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _target;

    private Transform _transform;
    public Vector3 Target => _transform.position;

    private void Start()
    {
        _camera = Camera.main;
        _transform = transform;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) NewPosition();
    }

    private void NewPosition()
    {
        _target = _camera.ScreenToWorldPoint(Input.mousePosition);
        _target.z = transform.position.z;

        _transform.position = Vector3.Lerp(_transform.position, _target, 5f * Time.deltaTime);
    }
}