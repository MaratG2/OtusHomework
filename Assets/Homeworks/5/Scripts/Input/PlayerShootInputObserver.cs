using Homeworks5.Hero;
using UnityEngine;

namespace Homeworks5.Input
{
    public class PlayerShootInputObserver : MonoBehaviour
    {
        //TODO: DI
        [SerializeField] private PlayerShootInput _playerShootnput;
        [SerializeField] private HeroModel _heroModel;

        private void OnEnable()
        {
            _playerShootnput.onShoot += Shoot;
        }
        
        private void OnDisable()
        {
            _playerShootnput.onShoot -= Shoot;
        }

        private void Shoot()
        {
            _heroModel.core.shooter.onShoot?.Invoke();
        }
    }
}