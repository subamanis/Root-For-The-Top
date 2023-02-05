using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObstacleGenerator : MonoBehaviour
{
    public GameObject goodObjectPrefab;
    public GameObject badObjectPrefab;
    public Transform obstaclesHolder;
    public float L = 5f;
    public float ANGLE = 70f;
    public float randomPointRandomizeFactor = 1f;
    public float spawnEverySeconds = 0.5f;
    private float secondsSinceLastSpawn = 0f;
    public Timer timer;
    private bool isSpawnDisabled = false;


    void disableSpawn()
    {
        isSpawnDisabled = true;
    }

    private void spawnObstacle()
    {
        var firstPointOnCircleX = transform.position.x - (L * Mathf.Sin(ANGLE));
        var firstPointOnCircleY = transform.position.y - (L * Mathf.Cos(ANGLE));

        var secondPointOnCircleX = transform.position.x - (L * Mathf.Sin(-ANGLE));
        var secondPointOnCircleY = transform.position.y - (L * Mathf.Cos(-ANGLE));

        while (true)
        {
            var randomPointX = Random.Range(transform.position.x - L + randomPointRandomizeFactor,
                transform.position.x + L + randomPointRandomizeFactor);
            var randomPointY =
                Random.Range(transform.position.y, transform.position.y - L - randomPointRandomizeFactor);
            var randomPointPosition = new Vector2(randomPointX, randomPointY);
            var randomPointDistanceFromOrigin = Mathf.Sqrt(Mathf.Abs(randomPointX - transform.position.x) +
                                                           Mathf.Abs(randomPointY - transform.position.y));

            var isAboveFirstLine =
                ((firstPointOnCircleX - transform.position.x) * (randomPointY - transform.position.y) -
                 (firstPointOnCircleY - transform.position.y) * (randomPointX - transform.position.x)) > 0;
            var isBelowSecondLine =
                ((secondPointOnCircleX - transform.position.x) * (randomPointY - transform.position.y) -
                 (secondPointOnCircleY - transform.position.y) * (randomPointX - transform.position.x)) < 0;

            if (isAboveFirstLine && isBelowSecondLine && randomPointDistanceFromOrigin < L)
            {
                if (Random.Range(1, 10) <= 5)
                {
                    Instantiate(badObjectPrefab, randomPointPosition, Quaternion.identity, obstaclesHolder);
                }
                else
                {
                    Instantiate(goodObjectPrefab, randomPointPosition, Quaternion.identity, obstaclesHolder);
                }

                break;
            }
        }
    }

    void Start()
    {
        spawnObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        secondsSinceLastSpawn += Time.deltaTime;

        if (!isSpawnDisabled && secondsSinceLastSpawn >= spawnEverySeconds)
        {
            spawnObstacle();
            secondsSinceLastSpawn = 0f;
        }
    }

    private void Awake()
    {
        timer.onTimeEnd += disableSpawn;
    }
}