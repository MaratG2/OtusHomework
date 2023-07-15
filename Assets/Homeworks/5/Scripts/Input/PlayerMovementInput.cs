using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DefaultInputActions = UnityEngine.InputSystem5.DefaultInputActions;

namespace Homeworks5.Input
{
    public class PlayerMovementInput : MonoBehaviour
    {
        private DefaultInputActions _inputActions;
        public event Action<Vector2> onMove;
        private bool _isMoving;
        private Vector2 _direction;

        private void Awake()
        {
            _inputActions = new DefaultInputActions();
        }
        
        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.Move.performed += OnMoveBegin;
            _inputActions.Player.Move.canceled += OnMoveEnd;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Player.Move.performed -= OnMoveBegin;
            _inputActions.Player.Move.canceled -= OnMoveEnd;
        }
        
        private void OnMoveBegin(InputAction.CallbackContext ctx)
        {
            _isMoving = true;
            _direction = ctx.ReadValue<Vector2>();
        }
        
        private void OnMoveEnd(InputAction.CallbackContext ctx)
        {
            _isMoving = false;
        }

        private void Update()
        {
            if (!_isMoving)
                return;
            
            onMove?.Invoke(_direction);
        }
    }
}