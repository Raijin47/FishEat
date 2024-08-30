using System.Collections.Generic;
using UnityEngine;

public class EnemyProvider
{
    private List<EnemyBase> _enemyBases = new();
    private readonly PoolInstantiateObject<EnemyBase> _poolInstantiateObject;
    private readonly EnemyFactory _enemyFactory;

    public EnemyProvider(PoolInstantiateObject<EnemyBase> poolInstantiateObject, Transform _content)
    {
        _enemyFactory = new EnemyFactory(poolInstantiateObject, _content);
        _poolInstantiateObject = poolInstantiateObject;
    }

    private void InitEnemy(EnemyBase enemyBase, EnemyData data)
    {
        enemyBase.gameObject.SetActive(true);
        enemyBase.Die += OnDie;
        enemyBase.Init(data);
    }

    private void OnDie(EnemyBase enemyBase)
    {
        Remove(enemyBase);
    }

    public void Dispose()
    {
        for (int i = 0; i < _enemyBases.Count; i++)
        {
            _enemyBases[i].Release();
        }
    }

    public void CreateEnemy(Vector3 position, EnemyData data)
    {
        var enemyData = _enemyFactory.Spawn(position);
        var enemyBase = enemyData.Item1;
        if (enemyBase == null)
            return;
        var isInstantiate = enemyData.Item2;
        if (isInstantiate) Add(enemyBase, data);
        else ResetPoolEnemy(enemyBase, data);
    }

    private void ResetPoolEnemy(EnemyBase enemyBase, EnemyData data)
    {
        enemyBase.gameObject.SetActive(true);
        enemyBase.ResetData(data);
    }

    public void Add(EnemyBase enemyBase, EnemyData data)
    {
        _enemyBases.Add(enemyBase);
        InitEnemy(enemyBase, data);
    }

    public void Remove(EnemyBase enemyBase)
    {
        enemyBase.gameObject.SetActive(false);
        _enemyBases.Remove(enemyBase);
        _poolInstantiateObject.Release(enemyBase);
    }
}