using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerCursor _cursor;
    private SpriteRenderer _sprite;
    private Transform _transform;

    public float Offset = 0.9f;

    public bool isSpeedBoost;

    public float rotationSpeed = 0.5f;
    public float speedStandart = 3;
    public float speedBoost = 8;

    public float inertiaDeceleration = 0.5f;

    private float _currentSpeed;
    private Coroutine _movementProcessCoroutine;


    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _transform = transform;
        _currentSpeed = speedStandart;
    }

    private void OnEnable()
    {
        GameController.StartGame += StartGame;
        GameController.GameOver += GameOver;
    }

    private void StartGame()
    {
        transform.position = Vector3.zero;
        if (_movementProcessCoroutine != null)
        {
            StopCoroutine(_movementProcessCoroutine);
            _movementProcessCoroutine = null;
        }
        _movementProcessCoroutine = StartCoroutine(MovementProcessCoroutine());
    }

    private void GameOver()
    {
        if (_movementProcessCoroutine != null)
        {
            StopCoroutine(_movementProcessCoroutine);
            _movementProcessCoroutine = null;
        }
        transform.position = new Vector3(0, -50);
    }

    private IEnumerator MovementProcessCoroutine()
    {
        while (true)
        {
            if (_cursor.move && Vector2.Distance(transform.position, _cursor.Target) > Offset)
            {
                _currentSpeed = isSpeedBoost ? speedBoost : speedStandart;
                Move();
            }
            else
            {
                ApplyInertia();
            }
            yield return null;
        }
    }

    private void ApplyInertia()
    {
        if (_currentSpeed > 0)
        {
            _currentSpeed -= inertiaDeceleration * Time.deltaTime;
            if (_currentSpeed < 0) _currentSpeed = 0;
            _transform.position += _currentSpeed * Time.deltaTime * transform.right;
        }
    }

    private void Move()
    {
        //_transform.position = Vector3.MoveTowards(_transform.position, _cursor.Target, _currentSpeed * Time.deltaTime);
        _transform.position += _currentSpeed * Time.deltaTime * transform.right;
        float angle = Mathf.Atan2(_cursor.Target.y - _transform.position.y, _cursor.Target.x - _transform.position.x) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(0f, 0f, angle), _currentSpeed * rotationSpeed * Time.deltaTime);
        _sprite.flipY = _transform.position.x > _cursor.Target.x;
    }

    public void BonusSpeedBoost(BonusType type)
    {
        if (type == BonusType.SpeedBoost)
        {
            isSpeedBoost = true;
            Invoke(nameof(EndBoost), Bonuses.Instance.timeSpeedBoost);
        }
    }

    public void EndBoost()
    {
        isSpeedBoost = false;
    }
}