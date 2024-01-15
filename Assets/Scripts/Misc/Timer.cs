using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer
{
    private float time;
    private bool canCount;
    private Dictionary<float, Action> timedAction = new Dictionary<float, Action>();

    public bool CanCount => canCount;

    public Timer()
    {
        //TimeManager.AddTimer(this);
    }

    public Timer (float startTime)
    {
        //TimeManager.AddTimer(this);
        time = startTime;
    }

    public float ElapsedTime
    {
        get => time;
    }

    /// <summary>
    /// Starts the timer.
    /// </summary>
    public void Start()
    {
        canCount = true;
    }

    /// <summary>
    /// Restart the timer without stopping.
    /// </summary>
    public void Restart()
    {
        canCount = true;
        time = 0;
    }
    
    /// <summary>
    /// Makes timer work. Use in Update method.
    /// </summary>
    /// <param name="time Step"></param>
    public void Evaluate(float value)
    {
        if(canCount) time += value;

        if (timedAction.Count > 0)
        {
            foreach (var a in timedAction)
            {
                if (time >= a.Key)
                {
                    if(time <= a.Key + value)
                        a.Value?.Invoke();
                    
                }
            }
        }
    }

    /// <summary>
    /// Stops the timer, return time to 0.
    /// </summary>
    public void Stop()
    {
        canCount = false;
        time = 0;
    }

    /// <summary>
    /// Pausing timer count.
    /// </summary>
    public void Pause()
    {
        canCount = canCount ? false : true;
    }

    public void AddSpecificAction(float time, Action action)
    {
        timedAction.Add(time, action);
    }
}
