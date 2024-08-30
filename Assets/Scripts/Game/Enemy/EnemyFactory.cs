using UnityEngine;

public class EnemyFactory
{
    private readonly PoolInstantiateObject<EnemyBase> _poolEnemy;
    private readonly Transform _content;

    public EnemyFactory(PoolInstantiateObject<EnemyBase> poolEnemy, Transform content)
    {
        _content = content;
        _poolEnemy = poolEnemy;
    }

    public (EnemyBase, bool) Spawn(Vector3 position)
    {
        var obj = _poolEnemy.GetInstantiate();
        if (obj.Item1 != null)
        {
            var transform = obj.Item1.transform;
            transform.parent = _content;
            transform.position = position;
        }
        return obj;
    }
}