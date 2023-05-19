using System;
using Homeworks.h1.GameManager;
using UnityEngine;
using UnityEngine.InputSystem;
using DefaultInputActions = UnityEngine.InputSystem.h1.DefaultInputActions;

namespace Homeworks.h1
{
    public class InputController : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGamePauseListener,
        IGameResumeListener
    {
        public Action<int> OnMoveToSide;
        private DefaultInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new DefaultInputActions();
            _inputActions.Disable();
        }

        private void ChangeTrackPerformed(InputAction.CallbackContext ctx)
        {
            var trackShift = ctx.ReadValue<Single>();
            OnMoveToSide?.Invoke((int)trackShift);
        }

        public void OnGameStart()
        {
            _inputActions.Enable();
            _inputActions.Player.ChangeTrack.performed += ChangeTrackPerformed;
        }

        public void OnGameEnd()
        {
            _inputActions.Disable();
            _inputActions.Player.ChangeTrack.performed -= ChangeTrackPerformed;
        }

        public void OnGamePause()
        {
            _inputActions.Disable();
        }

        public void OnGameResume()
        {
            _inputActions.Enable();
        }
    }
}