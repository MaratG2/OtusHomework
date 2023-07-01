using ShootEmUp.GameManagement;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Inputs
{
    public sealed class PlayerShootController : MonoBehaviour,
        IGameStartListener,
        IGameEndListener,
        IGameResumeListener,
        IGamePauseListener
    {
        private PlayerShootInput _input;
        private CharacterController _character;
        
        [Inject]
        private void Construct(PlayerShootInput input, CharacterController character)
        {
            this._input = input;
            this._character = character;
        }

        private void PrepareAndShoot()
        {
            _character._fireRequired = true;
        }

        public void OnGameStart()
        {
            _input.OnShoot += PrepareAndShoot;
        }

        public void OnGameEnd()
        {
            _input.OnShoot -= PrepareAndShoot;
        }

        public void OnGameResume()
        {
            _input.OnShoot += PrepareAndShoot;
        }

        public void OnGamePause()
        {
            _input.OnShoot -= PrepareAndShoot;
        }
    }
}