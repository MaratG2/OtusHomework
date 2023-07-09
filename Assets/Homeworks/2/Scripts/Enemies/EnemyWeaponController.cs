using ShootEmUp.Configs;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Enemies
{
    public class EnemyWeaponController : MonoBehaviour
    {
        [SerializeField] private BulletConfig _bulletConfig;
        private BulletSystem _bulletSystem;
        private WeaponComponent _weaponComponent;
        private GameObject _target;
        
        [Inject]
        private void Construct(BulletSystem bulletSystem, WeaponComponent weaponComponent)
        {
            this._bulletSystem = bulletSystem;
            this._weaponComponent = weaponComponent;
        }
        
        public void SetTarget(GameObject target)
        {
            this._target = target;
        }
        
        public void Fire()
        {
            var startPosition = _weaponComponent.Position;
            var direction = (Vector2) _target.transform.position - startPosition;
            var directionNormalized = direction.normalized;
       
            _bulletSystem.Fire(new BulletSystem.Args
            {
                physicsLayer = (int)_bulletConfig.PhysicsLayer,
                color = _bulletConfig.Color,
                damage = _bulletConfig.Damage,
                position = startPosition,
                velocity = directionNormalized * _bulletConfig.Speed
            });
        }
    }
}