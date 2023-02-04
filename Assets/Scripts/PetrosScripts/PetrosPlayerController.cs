using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class PetrosPlayerController : MonoBehaviour
{
    public event Action onPlayerDeath = delegate{};
    public event Action onSuccessfulTouch = delegate{};
    public event Action onMissedTouch = delegate{};
    public TextMeshProUGUI livesText;

    float speed = 1.5f;
    private int lives = 2;
    PetrosObstacle pendingGoodObstacle;

    private void Start() {
        livesText.text = "Lives: " + lives;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            HandleTouch();
        }
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "goodObstacle") {
            pendingGoodObstacle = other.gameObject.GetComponent<PetrosObstacle>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "goodObstacle") {
            if (pendingGoodObstacle) {
                onMissedTouch.Invoke();
                DamagePlayer();
                //TODO: Do something to the player (same gameObject)
            }
        }
    }

    private void HandleTouch()
    {
        if (pendingGoodObstacle) {
            onSuccessfulTouch.Invoke();
            pendingGoodObstacle = null;
        } else {
            onMissedTouch.Invoke();
            DamagePlayer();
            //TODO: Do something to the player (same gameObject)
        }
    }

    private void DamagePlayer() 
    {
        Debug.Log("player damaged");
        lives -= 1;
        livesText.text = "Lives: "+lives;
        if (lives == 0) {
            onPlayerDeath.Invoke();
            print("PLAYER DEEEAAAAD");
            speed = 0;
        }
    }
}
