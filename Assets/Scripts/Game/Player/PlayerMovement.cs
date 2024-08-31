using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerCursor _cursor;
    private SpriteRenderer _sprite;
    private Transform _transform;

    private const float Offset = 0.5f;

    public bool isSpeedBoost;

    public float speedStandart = 10;
    public float speedBoost = 15;

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
        if(_movementProcessCoroutine != null)
        {
            StopCoroutine(_movementProcessCoroutine);
            _movementProcessCoroutine = null;
        }
        transform.position = new Vector3 (0, -50);
    }

    private IEnumerator MovementProcessCoroutine()
    {
        while(true)
        {
            if (Vector2.Distance(transform.position, _cursor.Target) > Offset) Move();
            _currentSpeed = isSpeedBoost ? speedBoost : speedStandart;
            yield return null;
        }
    }

    private void Move()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _cursor.Target, _currentSpeed * Time.deltaTime);

        float angle = Mathf.Atan2(_cursor.Target.y - _transform.position.y, _cursor.Target.x - _transform.position.x) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Lerp(_transform.rotation, Quaternion.Euler(0f, 0f, angle), _currentSpeed * 0.5f * Time.deltaTime);
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