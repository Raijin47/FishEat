using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private Animator _bite;
    private int _health;

    public Death _deathEnemyPrefab;
    public Vector3 offsefText;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            _text.text = _health.ToString();
        }
    }

    private void OnEnable()
    {
        //GameController.StartGame += StartGame;
    }

    private void Start()
    {
        Health = 5;
    }

    private void Update()
    {
        _text.transform.position = transform.position + offsefText;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyBase enemy))
        {
            if (enemy.Health > Health)
            {
                if(!Bonuses.Spend(BonusType.Protection))
                {
                    Debug.Log("GameOver");
                    Audio.Play(ClipType.gameOver);
                    UI.Instance.SetPage(0);
                }
            }
            else
            {

    Instantiate(_deathEnemyPrefab, transform.position + Vector3.down*1.2f, Quaternion.identity).flipX = enemy.sprite.flipX;
                Health += enemy.Exp;
                enemy.Release();
                _bite.Play("Bite Animation", 0, 0);
                Audio.Play(ClipType.eat);
            }
        }
    }
}