using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.U2D;

// Dynamic modification of spline to follow the path of mouse movement.
// This script is just a simplified demo to demonstrate the idea.
namespace Thanasis
{
    public class DrawAutomatically : MonoBehaviour
    {
        public float minimumDistance = 1.0f;
        public float splineHeightMultiplier = 1.3f;
        public float splineHeightMin = 0.3f;
        public float spawnEverySeconds = .5f;
        public float spawnDistance = 1;
        public float playerNextAngle = 180;
        public float limitPlayerAngleLeft = 240;
        public float limitPlayerAngleRight = 120;

        private float timeSinceLastSpawn = 0f;

        private Vector3 lastPosition;
        public SpriteShapeController spriteShapeController;
        public Timer timer;
        private Vector3 lastPointPosition;

        private void Start()
        {
            timer.onTimeEnd += () =>
            {
                this.enabled = false;
            };
        }

        private static int NextIndex(int index, int pointCount)
        {
            return Mod(index + 1, pointCount);
        }

        private static int PreviousIndex(int index, int pointCount)
        {
            return Mod(index - 1, pointCount);
        }

        private static int Mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        private void OnDrawGizmos()
        {
            var lastPointPosition =
                spriteShapeController.spline.GetPosition(spriteShapeController.spline.GetPointCount() - 1);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(lastPointPosition, Quaternion.Euler(0, 0, playerNextAngle) * Vector3.down * spawnDistance);

            // Gizmos.color = Color.blue;
            // Gizmos.DrawRay(lastPointPosition,  Vector3.down * spawnDistance);
        }

        private void Smoothen(SpriteShapeController sc, int pointIndex)
        {
            Vector3 position = sc.spline.GetPosition(pointIndex);
            Vector3 positionNext = sc.spline.GetPosition(NextIndex(pointIndex, sc.spline.GetPointCount()));
            Vector3 positionPrev = sc.spline.GetPosition(PreviousIndex(pointIndex, sc.spline.GetPointCount()));
            Vector3 forward = gameObject.transform.forward;

            float scale = Mathf.Min((positionNext - position).magnitude, (positionPrev - position).magnitude) * 0.33f;

            Vector3 leftTangent = (positionPrev - position).normalized * scale;
            Vector3 rightTangent = (positionNext - position).normalized * scale;

            sc.spline.SetTangentMode(pointIndex, ShapeTangentMode.Continuous);
            SplineUtility.CalculateTangents(position, positionPrev, positionNext, forward, scale, out rightTangent,
                out leftTangent);

            sc.spline.SetLeftTangent(pointIndex, leftTangent);
            sc.spline.SetRightTangent(pointIndex, rightTangent);
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastSpawn += Time.deltaTime;
            var spline = spriteShapeController.spline;

            var nextPosition = lastPointPosition +
                               (Quaternion.Euler(0, 0, playerNextAngle) * Vector3.down * spawnDistance);

            if (timeSinceLastSpawn > spawnEverySeconds)
            {
                var newPointIndex = spline.GetPointCount();
                spline.InsertPointAt(spline.GetPointCount(), nextPosition);
                Smoothen(spriteShapeController, newPointIndex - 1);


                timeSinceLastSpawn = 0;
                lastPointPosition = nextPosition;
            }
            else
            {
                spline.SetPosition(spline.GetPointCount() - 1,
                    Vector3.Lerp(lastPointPosition, nextPosition,
                        timeSinceLastSpawn / spawnEverySeconds));

                if (spline.GetPointCount() > 2)
                {
                    Smoothen(spriteShapeController, spline.GetPointCount() - 2);
                }
            }

            spline.SetHeight(spline.GetPointCount() - 1,
                timer.GetTimeLeft01() * splineHeightMultiplier + splineHeightMin);
        }

        public void HandleUserInput(float transformModifier)
        {
            playerNextAngle += transformModifier;
            playerNextAngle = Mathf.Clamp(playerNextAngle, limitPlayerAngleLeft, limitPlayerAngleRight);
        }
    }
}