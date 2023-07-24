using Homeworks5.Components;
using Homeworks5.Hero;
using UnityEngine;
using Zenject;

namespace Homeworks5.Input
{
    public class PlayerShootInputObserver : MonoBehaviour
    {
        private PlayerShootInput _playerShootInput;
        private HeroEntity _heroEntity;

        [Inject]
        private void Construct(PlayerShootInput input, HeroEntity heroEntity)
        {
            this._playerShootInput = input;
            this._heroEntity = heroEntity;
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
            _heroEntity.Get<IShootComponent>().Shoot();
        }
    }
}