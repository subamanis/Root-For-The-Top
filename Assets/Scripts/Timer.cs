using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action onTimeEnd = delegate { };

    public float initialTimer = 10;

    public TextMeshProUGUI myText;
    private bool isTimerOn = false;
    private TimeSpan timeLeft;
    public bool debugReset = false;


    void UpdateTimer()
    {
        timeLeft = timeLeft.Add(TimeSpan.FromSeconds(-Time.deltaTime));
        myText.text = String.Format("{0:00.0}", timeLeft.TotalSeconds);
    }

    public void AddSecondsToTimer(int seconds)
    {
        timeLeft = timeLeft.Add(TimeSpan.FromSeconds(seconds));
    }

    public float GetTimeLeft01()
    {
        var timerLeft01 = (float)timeLeft.TotalMilliseconds / (initialTimer * 1000);
        Debug.Log("Thanasis " + timerLeft01);
        return timerLeft01;
    }

    public void ResetTimer()
    {
        timeLeft = TimeSpan.FromSeconds(initialTimer);
        isTimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (debugReset)
        {
            debugReset = false;
            ResetTimer();
        }

        if (isTimerOn)
        {
            if (timeLeft > TimeSpan.Zero)
            {
                UpdateTimer();
                print(timeLeft);
            }
            else
            {
                timeLeft = TimeSpan.Zero;
                isTimerOn = false;
                onTimeEnd.Invoke();
            }
        }
    }
}