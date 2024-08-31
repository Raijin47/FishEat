using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Action StartGame;
    public static Action GameOver;

    public void StartGameButton() => StartGame?.Invoke();
}
