using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _target;

    public Vector3 Target => _target;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) NewPosition();
    }

    private void NewPosition()
    {
        _target = _camera.ScreenToWorldPoint(Input.mousePosition);
        _target.z = transform.position.z;

        transform.position = _target;
    }
}