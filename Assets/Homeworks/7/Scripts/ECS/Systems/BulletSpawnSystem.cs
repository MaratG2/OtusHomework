using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Bullet;
using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct BulletSpawnSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BulletTag_C, RequireSpawn_C>> _bulletFilter;
        private readonly EcsPoolInject<View_C> _viewPool;
        private readonly EcsPoolInject<RequireSpawn_C> _requireSpawnPool;
        private readonly EcsPoolInject<Position_C> _positionPool;
        private readonly EcsPoolInject<Renderer_C> _rendererPool;
        private readonly EcsPoolInject<Rigidbody_C> _rigidbodyPool;
        private readonly EcsCustomInject<SharedData> _data;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _bulletFilter.Value)
            {
                ref var viewC = ref _viewPool.Value.Get(entity);
                ref var rigidbodyC = ref _rigidbodyPool.Value.Get(entity);
                ref var rendererC = ref _rendererPool.Value.Get(entity);
                
                var positionC = _positionPool.Value.Get(entity);
                Vector3 spawnPos = new Vector3(positionC.position.x, 0.5f, positionC.position.y);
                var bullet = Object.Instantiate
                    (_data.Value.prefabBullet, spawnPos, Quaternion.identity, _data.Value.parent);
                bullet.Init(systems.GetWorld());
                bullet.PackEntity(entity);
                viewC.view = bullet.gameObject;
                rendererC.renderer = bullet.GetComponent<Renderer>();
                rigidbodyC.rigidbody = bullet.GetComponent<Rigidbody>();
                _positionPool.Value.Del(entity);
                _requireSpawnPool.Value.Del(entity);
            }
        }
    }
}