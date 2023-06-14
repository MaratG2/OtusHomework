using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DefaultInputActions = UnityEngine.InputSystem.h2.DefaultInputActions;

namespace ShootEmUp.Inputs
{
    public sealed class PlayerShootInput : MonoBehaviour
    {
        public Action OnShoot;
        private DefaultInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new DefaultInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.Fire.performed += OnInputShoot;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Player.Fire.performed -= OnInputShoot;
        }

        private void OnInputShoot(InputAction.CallbackContext ctx)
        {
            OnShoot?.Invoke();
        }
    }
}