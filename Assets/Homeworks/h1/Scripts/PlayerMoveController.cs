using Sirenix.OdinInspector;
using UnityEngine;

namespace Homeworks.h1
{
    public class PlayerMoveController : MonoBehaviour
    {
        [SerializeField, Required] MoveInput _moveInput;
        [SerializeField, Required] PlayerRoadMover _playerRoadMover;

        private void OnEnable()
        {
            _moveInput.OnMoveToSide += _playerRoadMover.ChangeTrack;
        }

        private void OnDisable()
        {
            _moveInput.OnMoveToSide -= _playerRoadMover.ChangeTrack;
        }
    }
}