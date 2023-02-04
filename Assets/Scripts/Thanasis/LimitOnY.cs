using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LimitOnY : MonoBehaviour
{
    public enum LimitDirection
    {
        None,
        Up,
        Down
    }

    public LimitDirection limitDirection = LimitDirection.None;
    public float yLimit = 0;
    public Transform matchObject;
    private Vector3 startingDifference;

    private void Start()
    {
        startingDifference = transform.position - matchObject.position;
    }

    // Update is called once per frame
    void Update()
    {
        var fromPosition = transform.position;
        var toMatchObject = matchObject.position;

        if (limitDirection == LimitDirection.Down)
        {
            if (toMatchObject.y <= yLimit)
            {
                transform.position = new Vector3(fromPosition.x, yLimit, fromPosition.z);
            }
            else
            {
                transform.position = new Vector3(startingDifference.x, matchObject.position.y, startingDifference.z);
            }
        }
        else if (limitDirection == LimitDirection.Up)
        {
            if (toMatchObject.y >= yLimit)
            {
                transform.position = new Vector3(fromPosition.x, yLimit, fromPosition.z);
            }
            else
            {
                transform.position = new Vector3(startingDifference.x, matchObject.position.y, startingDifference.z);
            }
        }
    }
}