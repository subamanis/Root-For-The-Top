using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootCollider : MonoBehaviour
{
    private const float DEFAULT_TIME_AMOUNT = .5f;
    private SoundManager soundManager;


    public Timer timer;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("goodObstacle"))
        {
            Debug.Log("Good obstacle");
            soundManager.playSuccess();
            timer.AddSecondsToTimer(DEFAULT_TIME_AMOUNT);
        }
        else
        {
            Debug.Log("Bad obstacle");
            soundManager.playDamage();
            timer.AddSecondsToTimer(-DEFAULT_TIME_AMOUNT);
        }

        Destroy(other.gameObject);
    }
}