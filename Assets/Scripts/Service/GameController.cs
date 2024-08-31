using Neoxider;
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Spawner spawner;
    public static Action StartGame;
    public static Action GameOver;

    public void StartGameButton() => StartGame?.Invoke();

    public void GameOverButton() => GameOver?.Invoke();

    private void OnEnable()
    {
        StartGame += spawner.StartSpawn;
        GameOver += spawner.RemoveAllSpawnedObjects;
    }

    private void OnDisable()
    {
        StartGame -= spawner.StartSpawn;
        GameOver -= spawner.RemoveAllSpawnedObjects;
    }
}
