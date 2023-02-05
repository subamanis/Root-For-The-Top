using System.Collections;
using System.Collections.Generic;
using Thanasis;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public DrawAutomatically drawAutomatically;
    public float angleModifier = 1f; 

    public GameObject leftHand;
    public GameObject rightHand;
    private GameOrchestrator orchestrator;

    private void Awake() {
        orchestrator = FindObjectOfType<GameOrchestrator>();
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var transformModifier = (Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f) * 2;
            if (orchestrator._playerState == GameOrchestrator.InternalPlayerState.FirstIsPlaying) {
                if (transformModifier > 0) {
                    rightHand.SetActive(true);
                } else {
                    leftHand.SetActive(true);
                }
            }

            Debug.Log(transformModifier);
            drawAutomatically.HandleUserInput(transformModifier*angleModifier);
        } else {
            leftHand.SetActive(false);
            rightHand.SetActive(false);
        }
    }
}