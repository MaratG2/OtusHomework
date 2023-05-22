using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField, Required] MoveInput _moveInput;
        [SerializeField, Required] PlayerMover _playerMover;

        private void OnEnable()
        {
            _moveInput.OnMoveToSide += _playerMover.ChangeTrack;
        }

        private void OnDisable()
        {
            _moveInput.OnMoveToSide -= _playerMover.ChangeTrack;
        }
    }
}