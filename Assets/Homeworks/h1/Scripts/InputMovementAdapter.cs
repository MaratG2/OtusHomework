using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    public class InputMovementAdapter : MonoBehaviour
    {
        [SerializeField, Required] InputController _inputController;
        [SerializeField, Required] MovementController _movementController;

        private void OnEnable()
        {
            _inputController.OnMoveToSide += _movementController.ChangeTrack;
        }

        private void OnDisable()
        {
            _inputController.OnMoveToSide -= _movementController.ChangeTrack;
        }
    }
}