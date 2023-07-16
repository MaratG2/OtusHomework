using System.Linq;
using UnityEngine;
using ShootEmUp.Bullets;
using ShootEmUp.Level;
using ShootEmUp.Pool;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        private PoolSettings _poolSettings;
        private PoolFacade<Bullet> _poolFacade;
        public PoolFacade<Bullet> PoolFacade => _poolFacade;

        [Inject]
        private void Construct(PoolSettings poolSettings)
        {
            this._poolSettings = poolSettings;
        }
        
        private void Awake()
        {
            _poolFacade = new (_poolSettings);
        }

        public void Fire(BulletArgs args)
        {
            var bulletFromPool = _poolFacade.DePool();
            if (!bulletFromPool)
                bulletFromPool = _poolFacade.AddActive();
            
            bulletFromPool.Init(args, new PoolObject<Bullet>(_poolFacade));
        }
    }
}