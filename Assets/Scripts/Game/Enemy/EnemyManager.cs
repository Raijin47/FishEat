using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private Transform _content;
    [SerializeField] private EnemyLocator _enemyLocator;
    [SerializeField] private EnemyData[] data;

    private readonly WaitForSeconds IntervalSpawn = new(1f);
    private Camera _camera;

    private EnemyProvider _enemyProvider;
    private PoolInstantiateObject<EnemyBase> _instantiateObject;

    private void Awake()
    {
        Instance = this;
        _camera = Camera.main;
    }

    private void Start()
    {
        Init();        
        StartCoroutine(SpawnProcessCoroutine());
    }

    private void Init()
    {
        _instantiateObject = new PoolInstantiateObject<EnemyBase>(_enemyLocator.EnemyBase);
        _enemyProvider = new EnemyProvider(_instantiateObject, _content);
    }

    IEnumerator SpawnProcessCoroutine()
    {
        while (true)
        {
            yield return IntervalSpawn;
            Spawn(GetSpawnPoint());
        }
    }

    private void Spawn(Vector2 position)
    {
        int randomData = Random.Range(0, data.Length);
        _enemyProvider.CreateEnemy(position, data[randomData]);
    }

    private Vector2 GetSpawnPoint()
    {
        Vector2 point = new(Random.value > 0.5f ? -0.2f : 1.2f, 0);
        point.y = Random.value;

        return _camera.ViewportToWorldPoint(point); 
    }
}