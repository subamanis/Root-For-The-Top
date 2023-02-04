using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Thanasis;

public class EarthPlayerController : MonoBehaviour
{
    public event Action onPlayerDeath = delegate { };
    public event Action onSuccessfulTouch = delegate { };
    public event Action onMissedTouch = delegate { };

    // private int MOMENTUM_LOSS_MULTIPLIER = 6;
    // private float MAX_MOMENTUM = 1f;

    // private float speed = 1.5f;
    public float momentumGatheredPercent = .25f;
    public float momentumLostPercent = 0.4f;
    private PetrosObstacle pendingGoodObstacle;

    public DrawAutomatically drawAutomatically;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch();
        }
        // transform.Translate(0, (speed + momentumGathered) * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("goodObstacle"))
        {
            pendingGoodObstacle = other.gameObject.GetComponent<PetrosObstacle>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("goodObstacle"))
        {
            if (pendingGoodObstacle)
            {
                onMissedTouch.Invoke();
                DamagePlayer();
                //TODO: Do something to the player (same gameObject)
            }
            else
            {
                AddMomentum();
            }
        }
        else if (other.gameObject.CompareTag("badObstacle"))
        {
            AddMomentum();
        }
    }

    private void HandleTouch()
    {
        if (pendingGoodObstacle)
        {
            onSuccessfulTouch.Invoke();
            pendingGoodObstacle = null;
        }
        else
        {
            onMissedTouch.Invoke();
            DamagePlayer();
            //TODO: Do something to the player (same gameObject)
        }
    }

    private void DamagePlayer()
    {
        Debug.Log("player damaged");
        // livesText.text = "Lives: "+lives;
        LoseMomentum();
        // if (lives == 0) {
        // onPlayerDeath.Invoke();
        // print("PLAYER DEEEAAAAD");
        // speed = 0;
        // }
    }

    private void AddMomentum()
    {
        drawAutomatically.spawnDistance *= momentumGatheredPercent;
        // momentumGathered = Mathf.Clamp(momentumGathered + momentumDelta, 0, MAX_MOMENTUM);
    }

    private void LoseMomentum()
    {
        drawAutomatically.spawnDistance *= (1f - momentumLostPercent);
        // momentumGathered = Mathf.Clamp(momentumGathered - (momentumDelta * MOMENTUM_LOSS_MULTIPLIER), 0, MAX_MOMENTUM);
    }
}