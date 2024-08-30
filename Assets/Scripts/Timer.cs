using System;
using UnityEngine;

[Serializable]
public class Timer
{
    private float _requiredTime = 0;
    private float _currentTime = 0;

    public bool IsCompleted = false;
    public float CurrentTime => _currentTime;
    public float RequiredTime
    {
        get => _requiredTime;
        set
        {
            _requiredTime = value;
        }
    }

    public Timer(float timer)
    {
        _requiredTime = timer;
        _currentTime = _requiredTime;
    }

    public void Update()
    {
        if (IsCompleted)
        {
            return;
        }

        if (_currentTime > 0f)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0f)
            {
                _currentTime = 0;
                IsCompleted = true;
            }
        }
    }

    public void RestartTimer()
    {
        _currentTime = _requiredTime;
        IsCompleted = false;
    }
}