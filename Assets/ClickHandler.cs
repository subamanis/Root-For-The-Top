using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if (Camera.main.ScreenToViewportPoint(Input.mousePosition).x <= 0.5)
            {
                var transformModifier = Mathf.Abs(Camera.main.ScreenToViewportPoint(Input.mousePosition).x - 0.5f)*2;
                Debug.Log("Left");
                Debug.Log(transformModifier.ToString());
            }
            else
            {
                var transformModifier =( Mathf.Abs(Camera.main.ScreenToViewportPoint(Input.mousePosition).x) - 0.5) *2;
                Debug.Log("Right");
                Debug.Log(transformModifier.ToString());
            }
        }
    }
}
