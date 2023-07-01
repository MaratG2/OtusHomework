using System.Linq;
using UnityEngine;
using ShootEmUp.Bullets;
using ShootEmUp.Pool;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField] private PoolSettings _poolSettings;
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
            
            poolBullet.Init(args, new PoolObject<Bullet>(_poolFacade));
        }
        
        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
        }
    }
}