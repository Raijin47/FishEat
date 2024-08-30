using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;
    private int _health;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyBase enemy))
        {
            if (enemy.Health > Health)
            {
                Debug.Log("GameOver");
            }
            else
            {
                Health += enemy.Exp;
                enemy.Release();
            }
        }
    }
}