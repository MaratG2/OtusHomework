using UnityEngine;
using Zenject;

namespace ShootEmUp.Inputs
{
    public sealed class PlayerMovementController : MonoBehaviour
    {
        private PlayerMovementInput _input;
        private MoveComponent _movement;

        [Inject]
        private void Construct(PlayerMovementInput input, MoveComponent movement)
        {
            this._input = input;
            this._movement = movement;
        }

        private void OnEnable()
        {
            _input.OnMove += PrepareAndMove;
        }

        private void OnDisable()
        {
            _input.OnMove -= PrepareAndMove;
        }

        private void PrepareAndMove(Vector2 velocity)
        {
            velocity *= Time.fixedDeltaTime;
            _movement.MoveByRigidbodyVelocity(velocity);
        }
    }
}