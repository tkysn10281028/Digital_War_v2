using UnityEngine;

namespace DigitalWar.Cameras
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Vector3 _offset = new(0, 0, -10);
        [SerializeField] private float _smoothSpeed = 0.125f;

        void LateUpdate()
        {
            if (_player != null)
            {
                Vector3 desiredPosition = _player.position + _offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}