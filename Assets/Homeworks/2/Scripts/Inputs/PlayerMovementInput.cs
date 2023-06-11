using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DefaultInputActions = UnityEngine.InputSystem.h2.DefaultInputActions;

namespace ShootEmUp.Inputs
{
    public class PlayerMovementInput : MonoBehaviour
    {
        public Action<Vector2> OnMove;
        private DefaultInputActions _inputActions;
        private bool _isMoving;
        private Vector2 _moveDirection;

        private void Awake()
        {
            _inputActions = new DefaultInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
            _inputActions.Player.Move.performed += StartMove;
            _inputActions.Player.Move.canceled += EndMove;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
            _inputActions.Player.Move.performed -= StartMove;
            _inputActions.Player.Move.canceled -= EndMove;
        }

        private void StartMove(InputAction.CallbackContext ctx)
        {
            _moveDirection = ctx.ReadValue<Vector2>();
            _isMoving = true;
        }

        private void EndMove(InputAction.CallbackContext ctx)
        {
            _isMoving = false;
        }

        private void Update()
        {
            if (!_isMoving)
                return;
            OnMove?.Invoke(_moveDirection);
        }
    }
}