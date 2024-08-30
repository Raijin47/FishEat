using System;
using UnityEngine;
using TMPro;

public class EnemyBase : MonoBehaviour
{
    public event Action<EnemyBase> Die;

    private SpriteRenderer _sprite;
    private BoxCollider2D _collider;
    private TextMeshPro _text;

    private float _speed;

    private bool _isRight;

    public int Health { get; private set; }
    public int Exp { get; private set; }

    private void Update()
    {
        transform.position += _speed * Time.deltaTime * (_isRight ? Vector3.right : Vector3.left);
    }

    public void Init(EnemyData data)
    {
        _sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _text = GetComponentInChildren<TextMeshPro>();
        ResetData(data);
    }
    public void ResetData(EnemyData data)
    {
        _isRight = transform.position.x < 0;
        _sprite.flipX = _isRight;

        _sprite.sprite = data.Sprite;
        Health = data.Health;
        _collider.size = data.Size;
        _speed = data.Speed;
        Exp = data.Exp;
        _text.text = Health.ToString();
    }

    public void Release() => Die?.Invoke(this);
}