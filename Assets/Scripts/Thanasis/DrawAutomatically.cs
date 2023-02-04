using System;
using UnityEngine;
using UnityEngine.U2D;

// Dynamic modification of spline to follow the path of mouse movement.
// This script is just a simplified demo to demonstrate the idea.
namespace Thanasis
{
    public class DrawAutomatically : MonoBehaviour
    {
        public float minimumDistance = 1.0f;
        public float splineHeight = 1.3f;
        public float playerNextAngle = 180;
        public float spawnEveryMillis = .5f;
        public float spawnDistance =1;

        private float timeSinceLastSpawn = 0f;

        private Vector3 lastPosition;
        public SpriteShapeController spriteShapeController;

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

            Gizmos.color = Color.yellow;
            Debug.DrawRay(lastPointPosition, Vector3.down * 3);
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

            if (timeSinceLastSpawn > spawnEveryMillis)
            {
                var spline = spriteShapeController.spline;
                var lastPointPosition = spline.GetPosition(spline.GetPointCount() - 1);
                spline.InsertPointAt(spline.GetPointCount(), lastPointPosition + (Vector3.down * spawnDistance));
                var newPointIndex = spline.GetPointCount() - 1;
                Smoothen(spriteShapeController, newPointIndex - 1);

                spline.SetHeight(newPointIndex, splineHeight);
                timeSinceLastSpawn = 0;
            }
        }
    }
}