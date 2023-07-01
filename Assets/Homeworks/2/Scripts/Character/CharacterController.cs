using ShootEmUp.Configs;
using ShootEmUp.GameManagement;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject _character;
        [SerializeField] private BulletConfig _bulletConfig;
        private BulletSystem _bulletSystem;
        private GameManager _gameManager;
        
        public bool _fireRequired;

        [Inject]
        private void Construct(GameManager gameManager, BulletSystem bulletSystem)
        {
            this._gameManager = gameManager;
            this._bulletSystem = bulletSystem;
        }
        
        private void OnEnable()
        {
            _character.GetComponent<HitPointsComponent>().OnDeath += _gameManager.EndGame;
        }

        private void OnDisable()
        {
            _character.GetComponent<HitPointsComponent>().OnDeath -= _gameManager.EndGame;
        }
        
        private void FixedUpdate()
        {
            if (_fireRequired)
            {
                OnFlyBullet();
                _fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            var weapon = _character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                physicsLayer = (int)_bulletConfig.PhysicsLayer,
                color = _bulletConfig.Color,
                damage = _bulletConfig.Damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * _bulletConfig.Speed
            });
        }
    }
}