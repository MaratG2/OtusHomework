using Homeworks.h1.GameManagement;
using Homeworks.h1.SO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerRoadMover : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField, Required] private PlayerMovementConfig _playerMovementConfig;
        [SerializeField] private Vector2 _trackBorders = new(0, 2);
        private int _currentTrack = 1;

        private void Awake()
        {
            this.enabled = false;
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
            newPos += transform.right * trackShift * _playerMovementConfig.ShiftSize;
            transform.localPosition = newPos;
        }

        public void OnGameStart()
        {
            this.enabled = true;
        }

        public void OnGameEnd()
        {
            this.enabled = false;
        }

        public void OnGamePause()
        {
            this.enabled = false;
        }

        public void OnGameResume()
        {
            this.enabled = true;
        }
    }
}