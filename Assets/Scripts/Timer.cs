using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action onTimeEnd = delegate { };

    public TextMeshProUGUI myText;
    private bool isTimerOn = false;
    private TimeSpan timeLeft;

    private float timerDuration;

    void UpdateTimer()
    {
        timeLeft = timeLeft.Add(TimeSpan.FromSeconds(-Time.deltaTime));
        myText.text = String.Format("{0:00.0}", timeLeft.TotalSeconds);
    }

    public void AddSecondsToTimer(float seconds)
    {
        timeLeft = timeLeft.Add(TimeSpan.FromSeconds(seconds));
    }

    public float GetTimeLeft01()
    {
        var timerLeft01 = (float)timeLeft.TotalMilliseconds / (timerDuration * 1000);
        Debug.Log("Thanasis " + timerLeft01);
        return timerLeft01;
    }

    public void ResetTimer(float seconds)
    {
        timerDuration = seconds;
        timeLeft = TimeSpan.FromSeconds(seconds);
        isTimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
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