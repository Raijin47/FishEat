using UnityEngine;

public class EnemyLocator : MonoBehaviour
{
    [SerializeField] private EnemyBase enemyBase;

    public EnemyBase EnemyBase => enemyBase;
}