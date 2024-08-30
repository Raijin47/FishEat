using UnityEngine;

[CreateAssetMenu(fileName = "New Eat Data", menuName = "ScriptableObjects/EatData", order = 51)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Vector2 _size;
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    [SerializeField] private int _exp;

    public Sprite Sprite => _sprite;
    public Vector2 Size => _size;
    public float Speed => _speed;
    public int Health => _health;
    public int Exp => _exp;
}