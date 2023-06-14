using System.Linq;
using UnityEngine;
using Zenject;
using ShootEmUp.Bullets;
using ShootEmUp.Pool;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private PoolSettings _poolSettings;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private LevelBounds levelBounds;
        private PoolFacade<Bullet> _poolFacade;

        private void Awake()
        {
            _poolFacade = new PoolFacade<Bullet>(_poolSettings);
        }
        
        private void FixedUpdate()
        {
            var poolObjects = _poolFacade.GetAllActive();
            foreach (var poolObj in poolObjects.ToList())
            {
                if (!levelBounds.InBounds(poolObj.transform.position))
                    _poolFacade.EnPool(poolObj);
            }
        }

        public void FlyBulletByArgs(Args args)
        {
            var poolBullet = _poolFacade.DePool();
            if (!poolBullet)
                poolBullet = _poolFacade.AddActive();
            
            poolBullet.Init(args);
            
            poolBullet.OnCollisionEntered += this.OnBulletCollision;
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            this.RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnCollisionEntered -= this.OnBulletCollision;
            bullet.transform.SetParent(_poolSettings.Parent);
            _poolFacade.EnPool(bullet);
        }
        
        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }
    }
}