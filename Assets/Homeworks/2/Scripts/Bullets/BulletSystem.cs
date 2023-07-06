using System.Linq;
using UnityEngine;
using ShootEmUp.Bullets;
using ShootEmUp.Pool;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        private PoolSettings _poolSettings;
        private LevelBoundsController _levelBoundsController;
        private PoolFacade<Bullet> _poolFacade;

        [Inject]
        private void Construct(PoolSettings poolSettings,
            LevelBoundsController levelBoundsController)
        {
            this._poolSettings = poolSettings;
            this._levelBoundsController = levelBoundsController;
        }
        
        private void Awake()
        {
            _poolFacade = new (_poolSettings);
        }
        
        private void FixedUpdate()
        {
            CheckBulletsOutOfBounds();
        }
        
        private void CheckBulletsOutOfBounds()
        {
            var poolObjects = _poolFacade.GetAllActive();
            foreach (var poolObj in poolObjects.ToList())
            {
                if (!_levelBoundsController.IsInBounds(poolObj.transform.position))
                    _poolFacade.EnPool(poolObj);
            }
        }

        public void Fire(Args args)
        {
            var bulletFromPool = _poolFacade.DePool();
            if (!bulletFromPool)
                bulletFromPool = _poolFacade.AddActive();
            
            bulletFromPool.Init(args, new PoolObject<Bullet>(_poolFacade));
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