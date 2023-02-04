using System.Collections;
using System.Collections.Generic;
using Thanasis;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public DrawAutomatically drawAutomatically;
    public float angleModifier = 1f; 
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var transformModifier = (Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f) * 2;

            Debug.Log(transformModifier);
            drawAutomatically.HandleUserInput(transformModifier*angleModifier);
        }
    }
}