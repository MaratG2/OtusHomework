using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    public class InputMovementAdapter : MonoBehaviour
    {
        [SerializeField, Required] MoveInput _moveInput;
        [SerializeField, Required] MovementController _movementController;

        private void OnEnable()
        {
            _moveInput.OnMoveToSide += _movementController.ChangeTrack;
        }

        private void OnDisable()
        {
            _moveInput.OnMoveToSide -= _movementController.ChangeTrack;
        }
    }
}