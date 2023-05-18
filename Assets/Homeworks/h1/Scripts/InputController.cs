using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DefaultInputActions = UnityEngine.InputSystem.h1.DefaultInputActions;

namespace Homeworks.h1
{
    public class InputController : MonoBehaviour
    {
        public Action<int> OnMoveToSide;
        private DefaultInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new DefaultInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.ChangeTrack.performed += ChangeTrackPerformed;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Player.ChangeTrack.performed -= ChangeTrackPerformed;
        }

        private void ChangeTrackPerformed(InputAction.CallbackContext ctx)
        {
            var trackShift = ctx.ReadValue<Single>();
            OnMoveToSide?.Invoke((int)trackShift);
        }
    }
}