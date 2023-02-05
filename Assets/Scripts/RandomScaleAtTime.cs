using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomScaleAtTime : MonoBehaviour
{
    public float maxTimeToScale = 1f;
    public float maxScaleToReach = 1f;
    public AnimationCurve scaleCurve = AnimationCurve.Linear(0, 0, 1, 1);
    private float scaleDuring;
    private float scaleToFactor;
    private float timeRunning;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        scaleToFactor = Random.Range(0, maxScaleToReach);
        scaleDuring = Random.Range(0, maxTimeToScale);

        timeRunning = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRunning < scaleDuring)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * scaleToFactor,
                scaleCurve.Evaluate(timeRunning / scaleDuring));

            timeRunning += Time.deltaTime;
        }
    }
}