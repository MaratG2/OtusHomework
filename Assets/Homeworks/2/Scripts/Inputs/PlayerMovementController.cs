using ShootEmUp.GameManagement;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Inputs
{
    public sealed class PlayerMovementController : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGameResumeListener,
        IGamePauseListener
    {
        private PlayerMovementInput _input;
        private MoveComponent _movement;

        [Inject]
        private void Construct(PlayerMovementInput input, MoveComponent movement)
        {
            this._input = input;
            this._movement = movement;
        }

        private void PrepareAndMove(Vector2 velocity)
        {
            velocity *= Time.fixedDeltaTime;
            _movement.MoveByRigidbodyVelocity(velocity);
        }

        public void OnGameStart()
        {
            _input.OnMove += PrepareAndMove;
        }

        public void OnGameEnd()
        {
            _input.OnMove -= PrepareAndMove;
        }

        public void OnGameResume()
        {
            _input.OnMove += PrepareAndMove;
        }

        public void OnGamePause()
        {
            _input.OnMove -= PrepareAndMove;
        }
    }
}