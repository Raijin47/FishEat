using UnityEngine;

public class EnemyCleaner : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyBase enemy))
            enemy.Release();
    }
}