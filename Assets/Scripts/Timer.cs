using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool canRun = false;
    private float timer; // Zamanlayıcı değeri
    private Action function;

    void Update()
    {
        if (canRun)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                canRun = false;
                Execute();
            }
        }
    }

    public bool StartTimer(float countDownTime, Action function)
    {
        this.function = function;

        if (!canRun)
        {
            timer = countDownTime;
            canRun = true;
            return true;
        }
        return false;
    }

    private void Execute()
    {
        function();
    }
}
