using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DefaultInputActions = UnityEngine.InputSystem5.DefaultInputActions;

namespace Homeworks5.Input
{
    public class PlayerShootInput : MonoBehaviour
    {
        private DefaultInputActions _inputActions;
        public event Action onShoot;
        
        private void Awake()
        {
            _inputActions = new DefaultInputActions();
        }
        
        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.Fire.performed += OnShoot;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Player.Fire.performed -= OnShoot;
        }
        
        private void OnShoot(InputAction.CallbackContext ctx)
        {
            onShoot?.Invoke();
        }
    }
}