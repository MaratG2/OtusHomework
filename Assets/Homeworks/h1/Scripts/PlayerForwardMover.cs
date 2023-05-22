using Homeworks.h1.GameManagement;
using Homeworks.h1.SO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerForwardMover : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGamePauseListener,
        IGameResumeListener
    {
        [SerializeField, Required] private PlayerMovementConfig _playerMovementConfig;
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
            _rb.velocity = transform.forward * _playerMovementConfig.Speed;
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