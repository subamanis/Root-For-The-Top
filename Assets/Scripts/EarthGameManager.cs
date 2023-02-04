using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthGameManager : MonoBehaviour
{
    private EarthPlayerController playerController;
    //private GeorgeTimer timer;

    void Awake()
    {
        playerController = FindObjectOfType<EarthPlayerController>();
        // timer = FindObjectOfType<GeorgeTimer>();
    }

    void Start()
    {
        playerController.onSuccessfulTouch += HandleSuccessFulTouch;
        playerController.onMissedTouch += HandleMissedTouch;
        //timer.onTimeEnd += HandleOnTimeEnd();
    }

    public void HandleSuccessFulTouch()
    {
        //TODO: play sound
    }

    public void HandleMissedTouch()
    {
        //TODO: play sound
    }

    public void HandleOnTimeEnd() 
    {

    }
}
