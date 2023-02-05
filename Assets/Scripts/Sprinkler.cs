using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Random = UnityEngine.Random;

namespace SpriteShapeExtras
{
    public class Sprinkler : MonoBehaviour
    {
        public GameObject m_Prefab;
        public float m_RandomFactor = 10.0f;
        public bool m_UseNormals = false;

        public float sprinkleEverySeconds = 0.5f;

        public bool sprinkleNow = false;

        public SpriteShapeController spriteShapeController;
        public Transform leavesHolder;

        private float timeSinceLastSpawn = 0f;

        float Angle(Vector3 a, Vector3 b)
        {
            float dot = Vector3.Dot(a, b);
            float det = (a.x * b.y) - (b.x * a.y);
            return Mathf.Atan2(det, dot) * Mathf.Rad2Deg;
        }

        // Use this for initialization. Plant the Prefabs on Startup
        void SprinkleNow()
        {
            Spline spl = spriteShapeController.spline;

            for (int i = Math.Max(1, spl.GetPointCount() - 6); i < spl.GetPointCount() - 1; ++i)
            {
                var leftOrRightSide = Random.Range(0f, 1f) > .5f;
                if (Random.Range(0, 100) > (100 - m_RandomFactor))
                {
                    var go = GameObject.Instantiate(m_Prefab, leavesHolder);
                    go.transform.position = spl.GetPosition(i);

                    if (m_UseNormals)
                    {
                        Vector3 lt = Vector3.Normalize(spl.GetPosition(i - 1) - spl.GetPosition(i));
                        Vector3 rt = Vector3.Normalize(spl.GetPosition(i + 1) - spl.GetPosition(i));
                        float a = Angle(Vector3.up, lt);
                        float b = Angle(lt, rt);
                        float c = a + (b * 0.5f);
                        if (b > 0)
                            c = (180 + c);
                        go.transform.rotation = Quaternion.Euler(0, 0, c * (leftOrRightSide ? 1 : -1));
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn > sprinkleEverySeconds)
            {
                SprinkleNow();
                sprinkleNow = false;
                timeSinceLastSpawn = 0f;
            }
        }
    }
}