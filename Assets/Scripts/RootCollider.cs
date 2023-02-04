using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootCollider : MonoBehaviour
{
    private const int DEFAULT_TIME_AMOUNT = 2;
    public Timer timer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("goodObstacle"))
        {
            Debug.Log("Good obstacle");
            timer.AddSecondsToTimer(DEFAULT_TIME_AMOUNT);
        }
        else
        {
            Debug.Log("Bad obstacle");
            timer.AddSecondsToTimer(-DEFAULT_TIME_AMOUNT);
        }

        Destroy(other.gameObject);
    }
}