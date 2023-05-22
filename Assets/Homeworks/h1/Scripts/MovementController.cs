using Homeworks.h1.GameManagement;
using Homeworks.h1.SO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField, Required] private MovementDataSO _movementData;
        [SerializeField] private Vector2 _trackBorders = new(0, 2);
        private int _currentTrack = 1;
        private Rigidbody _rb;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            this.enabled = false;
        }

        private void FixedUpdate()
        {
            MoveForward();
        }

        private void MoveForward()
        {
            _rb.velocity = transform.forward * _movementData.Speed;
        }

        public void ChangeTrack(int trackShift)
        {
            int newTrack = _currentTrack + trackShift;
            if (newTrack < _trackBorders.x || newTrack > _trackBorders.y)
                trackShift = 0;
            _currentTrack += trackShift;
            MoveToTrack(trackShift);
        }

        private void MoveToTrack(int trackShift)
        {
            Vector3 newPos = transform.localPosition;
            newPos += transform.right * trackShift * _movementData.ShiftSize;
            transform.localPosition = newPos;
        }

        public void OnGameStart()
        {
            this.enabled = true;
        }

        public void OnGameEnd()
        {
            this.enabled = false;
            _rb.velocity = Vector3.zero;
        }

        public void OnGamePause()
        {
            this.enabled = false;
            _rb.velocity = Vector3.zero;
        }

        public void OnGameResume()
        {
            this.enabled = true;
        }
    }
}