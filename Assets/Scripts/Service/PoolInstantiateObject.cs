using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolInstantiateObject<T> where T : Object
{
    public List<T> _usedPoolInstantiate = new();
    public List<T> _freePoolInstantiate = new();

    public T _data;

    public PoolInstantiateObject(T data)
    {
        _data = data;
    }

    public T Instantiate()
    {
        return Object.Instantiate(_data);
    }

    public (T, bool) GetInstantiate()
    {
        if (_freePoolInstantiate.Count <= 0)
        {
            var element = Instantiate();
            _usedPoolInstantiate.Add(element);
            return (element, true);
        }
        var elementGet = _freePoolInstantiate[^1];
        _freePoolInstantiate.Remove(elementGet);
        _usedPoolInstantiate.Add(elementGet);

        return (elementGet, false);
    }

    public void Release(T data)
    {
        if (data is EnemyBase enemy)
        {
            enemy.gameObject.SetActive(false);
        }
        else if (data is GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Data is not a GameObject or Enemy.");
        }

        _usedPoolInstantiate.Remove(data);
        _freePoolInstantiate.Add(data);
    }

    internal void Clear()
    {
        while (_usedPoolInstantiate.Count > 0)
            Release(_usedPoolInstantiate[0]);
    }
}