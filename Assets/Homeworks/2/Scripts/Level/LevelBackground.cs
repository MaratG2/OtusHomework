using UnityEngine;

namespace ShootEmUp.Level
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] public float _startPositionY;
        [SerializeField] public float _endPositionY;
        [SerializeField] public float _movingSpeedY;
        
        private void Awake()
        {
            ResetPos();
        }

        private void Update()
        {
            if (transform.position.y <= _endPositionY)
                ResetPos();
            
            transform.position -= new Vector3
            (
                transform.position.x,
                _movingSpeedY * Time.deltaTime,
                transform.position.z
            );
        }

        private void ResetPos()
        {
            transform.position = new Vector3
            (
                transform.position.x,
                _startPositionY,
                transform.position.z
            );
        }
    }
}