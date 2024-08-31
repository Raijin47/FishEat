using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private Transform _content;
    [SerializeField] private EnemyLocator _enemyLocator;
    [SerializeField] private EnemyData[] data;

    private readonly WaitForSeconds IntervalSpawn = new(0.8f);
    private readonly WaitForSeconds IntervalSpawnBonus = new(0.25f);
    private readonly WaitForSeconds IntervalComplexity = new(20f);
    private Camera _camera;
    private int _currentComplexity;

    private EnemyProvider _enemyProvider;
    private PoolInstantiateObject<EnemyBase> _instantiateObject;

    private Coroutine _spawnProcessCorotune;
    private Coroutine _increaseComplexityCoroutine;

    public bool isFishTraffic;

    private void Awake()
    {
        Instance = this;
        _camera = Camera.main;
    }

    private void Start()
    {
        _currentComplexity = 1;
        Init();

        if (_spawnProcessCorotune != null)
        {
            StopCoroutine(_spawnProcessCorotune);
            _spawnProcessCorotune = null;
        }
        _spawnProcessCorotune = StartCoroutine(SpawnProcessCoroutine());
    }

    private void OnEnable()
    {
        GameController.StartGame += StartGame;
        GameController.GameOver += GameOver;
    }

    private void StartGame()
    {
        if (_increaseComplexityCoroutine != null)
        {
            StopCoroutine(_increaseComplexityCoroutine);
            _increaseComplexityCoroutine = null;
        }
        _increaseComplexityCoroutine = StartCoroutine(IncreaseComplexity());
    }

    private void GameOver()
    {
        if (_increaseComplexityCoroutine != null)
        {
            StopCoroutine(_increaseComplexityCoroutine);
            _increaseComplexityCoroutine = null;
        }

        _currentComplexity = 1;
    }

    private IEnumerator IncreaseComplexity()
    {
        _currentComplexity = 1;
        while (_currentComplexity != data.Length)
        {
            _currentComplexity++;
            yield return IntervalComplexity;
        }
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
            yield return isFishTraffic ? IntervalSpawnBonus : IntervalSpawn;
            Spawn(GetSpawnPoint());
        }
    }

    private void Spawn(Vector2 position)
    {
        int randomData = Random.Range(0, _currentComplexity);
        _enemyProvider.CreateEnemy(position, data[randomData]);
    }

    private Vector2 GetSpawnPoint()
    {
        Vector2 point = new(Random.value > 0.5f ? -0.2f : 1.2f, 0);
        point.y = Random.value;

        return _camera.ViewportToWorldPoint(point);
    }

    public void BonusFishTraffic(BonusType type)
    {
        if (type == BonusType.FishTraffic)
        {
            isFishTraffic = true;
            Invoke(nameof(EndBoost), Bonuses.Instance.timeFishTraffic);
        }
    }

    public void EndBoost()
    {
        isFishTraffic = false;
    }
}