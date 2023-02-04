using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeorgeTimer : MonoBehaviour
{
    public event Action onTimeEnd = delegate { };
    
    public TextMeshProUGUI myText;
    private const int INITIAL_TIMER_SECONDS = 30;
    private bool isTimerOn = false;
    private TimeSpan timeLeft;
    public bool debugReset = false;


    void updateTimer()
    {
        timeLeft = timeLeft.Add(TimeSpan.FromSeconds(-Time.deltaTime));
        myText.text = String.Format("{0:00.0}", timeLeft.TotalSeconds);
    }

    public void addSecondsToTimer(int seconds)
    {
        timeLeft = timeLeft.Add(TimeSpan.FromSeconds(5));
    }

    public void resetTimer()
    {
        timeLeft = TimeSpan.FromSeconds(INITIAL_TIMER_SECONDS);
        isTimerOn = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(debugReset)
        {
            debugReset = false;
            resetTimer();
        }

        if(isTimerOn)
        {
            if(timeLeft > TimeSpan.Zero)
            {
                updateTimer();
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
