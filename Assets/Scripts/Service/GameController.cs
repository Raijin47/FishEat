using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Action StartGame;

    public void StartGameButton() => StartGame?.Invoke();

}
