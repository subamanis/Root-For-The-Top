using UnityEngine;

namespace Thanasis
{
    public class CameraFollow : MonoBehaviour
    {
        public float smoothness;
        public Transform targetObject;
        private Vector3 _initialOffset;
        private Vector3 _cameraPosition;

        void Start()
        {
            _initialOffset = transform.position - targetObject.position;
        }

        void FixedUpdate()
        {
            _cameraPosition = targetObject.position + _initialOffset;
            transform.position = Vector3.Lerp(transform.position, _cameraPosition, smoothness * Time.fixedDeltaTime);
        }
    }
}