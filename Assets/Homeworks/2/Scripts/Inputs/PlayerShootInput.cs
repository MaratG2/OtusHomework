using System;
using ShootEmUp.GameManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using DefaultInputActions = UnityEngine.InputSystem.h2.DefaultInputActions;

namespace ShootEmUp.Inputs
{
    public sealed class PlayerShootInput : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGameResumeListener,
        IGamePauseListener
    {
        public Action OnShoot;
        private DefaultInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new DefaultInputActions();
        }

        private void OnInputShoot(InputAction.CallbackContext ctx)
        {
            OnShoot?.Invoke();
        }

        public void OnGameStart()
        {
            _inputActions.Enable();
            _inputActions.Player.Fire.performed += OnInputShoot;
        }

        public void OnGameEnd()
        {
            _inputActions.Disable();
            _inputActions.Player.Fire.performed -= OnInputShoot;
        }

        public void OnGameResume()
        {
            _inputActions.Enable();
        }

        public void OnGamePause()
        {
            _inputActions.Disable();
        }
    }
}