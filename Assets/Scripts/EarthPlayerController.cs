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

    public float momentumGatheredPercent = .25f;
    public float momentumLostPercent = 0.4f;
    private GameObject pendingGoodObstacle;

    public DrawAutomatically drawAutomatically;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch();
        }
    }

    private void HandleTouch()
    {
        if (pendingGoodObstacle)
        {
            onSuccessfulTouch.Invoke();
            pendingGoodObstacle = null;
            soundManager.playSuccess();
            Destroy(pendingGoodObstacle);
            AddMomentum();
        }
        else
        {
            onMissedTouch.Invoke();
            DamagePlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("goodObstacle"))
        {
            pendingGoodObstacle = other.gameObject;
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
                soundManager.playDamage();
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("badObstacle"))
        {
            AddMomentum();
            soundManager.playSuccess();
            Destroy(other.gameObject);
        }
    }

    private void DamagePlayer()
    {
        LoseMomentum();
    }

    private void AddMomentum()
    {
        drawAutomatically.spawnDistance *= momentumGatheredPercent;
    }

    private void LoseMomentum()
    {
        drawAutomatically.spawnDistance *= (1f - momentumLostPercent);
    }
}