using Homeworks5.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks5.Input
{
    public class PlayerShootInputObserver : MonoBehaviour
    {
        private PlayerShootInput _playerShootInput;
        private HeroModel _heroModel;

        [Inject]
        private void Construct(PlayerShootInput input, HeroModel heroModel)
        {
            this._playerShootInput = input;
            this._heroModel = heroModel;
        }
        
        private void OnEnable()
        {
            _playerShootInput.onShoot += Shoot;
        }
        
        private void OnDisable()
        {
            _playerShootInput.onShoot -= Shoot;
        }

        private void Shoot()
        {
            _heroModel.core.shooter.onShoot?.Invoke();
        }
    }
}