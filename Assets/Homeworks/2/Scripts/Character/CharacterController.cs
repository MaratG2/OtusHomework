using ShootEmUp.Configs;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public bool _fireRequired;

        private void OnEnable()
        {
            character.GetComponent<HitPointsComponent>().OnDeath += gameManager.FinishGame;
        }

        private void OnDisable()
        {
            character.GetComponent<HitPointsComponent>().OnDeath -= gameManager.FinishGame;
        }
        
        private void FixedUpdate()
        {
            if (this._fireRequired)
            {
                this.OnFlyBullet();
                this._fireRequired = false;
            }
        }

        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) this._bulletConfig.PhysicsLayer,
                color = this._bulletConfig.Color,
                damage = this._bulletConfig.Damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this._bulletConfig.Speed
            });
        }
    }
}