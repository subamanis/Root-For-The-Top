using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Thanasis
{
    public class CameraFollow : MonoBehaviour
    {
        public event Action OnReadyToFollow = delegate { };

        public float speed = 3;
        public float chaseSpeed = 25;
        public Transform targetObject;
        private Vector3 _initialOffset;
        private Vector3 _cameraPosition;

        private bool _isFollowing = false;
        public bool isChasing = false;

        public float equalDistance = 0.1f;

        public void StartFollow()
        {
            _initialOffset = new Vector3(0, 0, (transform.position - targetObject.position).z);
            _isFollowing = true;
        }

        void Update()
        {
            if (_isFollowing)
            {
                _cameraPosition = targetObject.position + _initialOffset;
                transform.position =
                    Vector3.MoveTowards(transform.position, _cameraPosition,
                        (isChasing ? chaseSpeed : speed) * Time.deltaTime);
                // transform.position = Vector3.Lerp(transform.position, _cameraPosition, smoothness * Time.fixedDeltaTime);

                if (Vector3.Distance(_cameraPosition, transform.position) <= equalDistance)
                {
                    OnReadyToFollow.Invoke();
                    OnReadyToFollow = delegate { };
                }
            }
        }
    }
}