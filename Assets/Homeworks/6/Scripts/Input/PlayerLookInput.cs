using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DefaultInputActions = UnityEngine.InputSystem6.DefaultInputActions;

namespace Homeworks6.Input
{
    public class PlayerLookInput : MonoBehaviour
    {
        private DefaultInputActions _inputActions;
        public event Action<Vector2> onLook;
        
        private void Awake()
        {
            _inputActions = new DefaultInputActions();
        }
        
        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.Look.performed += OnLook;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Player.Look.performed -= OnLook;
        }
        
        private void OnLook(InputAction.CallbackContext ctx)
        {
            var lookPos = ctx.ReadValue<Vector2>();
            onLook?.Invoke(lookPos);
        }
    }
}