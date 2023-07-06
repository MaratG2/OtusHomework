using ShootEmUp.Configs;
using UnityEngine;
using Zenject;

namespace ShootEmUp.Character
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private BulletConfig _bulletConfig;
        private BulletSystem _bulletSystem;
        private WeaponComponent _weaponComponent;
        
        [Inject]
        private void Construct(BulletSystem bulletSystem, WeaponComponent weaponComponent)
        {
            this._bulletSystem = bulletSystem;
            this._weaponComponent = weaponComponent;
        }
        
        public void Fire()
        {
            _bulletSystem.Fire(new BulletSystem.Args
            {
                physicsLayer = (int)_bulletConfig.PhysicsLayer,
                color = _bulletConfig.Color,
                damage = _bulletConfig.Damage,
                position = _weaponComponent.Position,
                velocity = _weaponComponent.Rotation * Vector3.up * _bulletConfig.Speed
            });
        }
    }
}