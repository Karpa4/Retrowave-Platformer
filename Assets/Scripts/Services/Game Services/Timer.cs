using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Features.Services.Coroutine;

public class Timer
{
    private float currentTime;
    private const float Interval = 0.05f;

    public event Action<float> RefreshTime; 

    public Timer(ICoroutineRunner coroutineRunner)
    {
        currentTime = 0;
        coroutineRunner.StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Interval);
            currentTime += Interval;
            RefreshTime?.Invoke(currentTime);
        }
    }

    public float GetTime()
    {
        return currentTime;
    }
}
