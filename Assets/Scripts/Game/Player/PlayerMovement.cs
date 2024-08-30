using System;
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

    public float Speed { get; set; }

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _transform = transform;
        Speed = speedStandart;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _cursor.Target) > Offset) Move();

        Speed = isSpeedBoost ? speedBoost : speedStandart;
    }

    private void Move()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _cursor.Target, Speed * Time.deltaTime);

        float angle = Mathf.Atan2(_cursor.Target.y - _transform.position.y, _cursor.Target.x - _transform.position.x) * Mathf.Rad2Deg;
        _transform.rotation = Quaternion.Euler(0f, 0f, angle);
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