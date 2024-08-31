using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public ParticleSystem _bubleParticle;
    public event Action<EnemyBase> Die;

    public SpriteRenderer sprite;
    private BoxCollider2D _collider;
    private TextMeshPro _text;

    private float _speed;

    private bool _isRight;

    public int Health { get; private set; }
    public int Exp { get; private set; }

    private Coroutine _updateMovementProcess;

    public void Init(EnemyData data)
    {
        sprite = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _text = GetComponentInChildren<TextMeshPro>();
        ResetData(data);
    }

    public void ResetData(EnemyData data)
    {
        _bubleParticle.Play();

        _isRight = transform.position.x < 0;
        sprite.flipX = _isRight;

        sprite.sprite = data.Sprite;
        Health = data.Health;
        _collider.size = data.Size;
        _speed = data.Speed;
        Exp = data.Exp;
        _text.text = Health.ToString();

        if(_updateMovementProcess != null)
        {
            StopCoroutine(_updateMovementProcess);
            _updateMovementProcess = null;
        }
        _updateMovementProcess = StartCoroutine(UpdateMovementProcess());
    }

    public void Release()
    {
        _bubleParticle.Stop();
        Die?.Invoke(this);

        StopCoroutine(_updateMovementProcess);
        _updateMovementProcess = null;
    }

    private IEnumerator UpdateMovementProcess()
    {
        while(true)
        {
            transform.position += _speed * Time.deltaTime * (_isRight ? Vector3.right : Vector3.left);
            yield return null;
        }
    }
}