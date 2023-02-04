using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeorgeRootCollider : MonoBehaviour
{
    private const int DEFAULT_TIME_AMOUNT = 2;

    private GeorgeTimer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<GeorgeTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "goodObject")
        {
            timer.addSecondsToTimer(DEFAULT_TIME_AMOUNT);
        }
        else
        {
            timer.subSecondsFromTimer(DEFAULT_TIME_AMOUNT);
        }
        Destroy(other.gameObject);
    }
}
