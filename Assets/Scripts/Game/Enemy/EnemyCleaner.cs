using UnityEngine;

public class EnemyCleaner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyBase enemy))
            enemy.Release();
    }
}