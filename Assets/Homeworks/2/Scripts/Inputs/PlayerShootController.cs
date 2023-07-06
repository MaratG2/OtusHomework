using ShootEmUp.Character;
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
        private WeaponController _playerWeapon;
        
        [Inject]
        private void Construct(PlayerShootInput input, WeaponController playerWeapon)
        {
            this._input = input;
            this._playerWeapon = playerWeapon;
        }

        private void PrepareAndShoot()
        {
            _playerWeapon.Fire();
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