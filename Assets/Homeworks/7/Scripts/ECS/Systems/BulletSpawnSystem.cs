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
        private readonly EcsFilterInject<Inc<BulletView_C, Position_C, Team_C>> _bulletViewFilter;
        private readonly EcsPoolInject<BulletView_C> _bulletViewPool;
        private readonly EcsPoolInject<Position_C> _positionPool;
        private readonly EcsPoolInject<Renderer_C> _rendererPool;
        private readonly EcsCustomInject<SharedData> _data;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _bulletViewFilter.Value)
            {
                ref var bulletViewC = ref _bulletViewPool.Value.Get(entity);
                ref var rendererC = ref _rendererPool.Value.Get(entity);
                
                if (bulletViewC.isSpawn)
                {
                    bulletViewC.isSpawn = false;
                    var positionC = _positionPool.Value.Get(entity);
                    Vector3 spawnPos = new Vector3(positionC.position.x, 0.5f, positionC.position.y);
                    var bullet = Object.Instantiate
                        (_data.Value.prefabBullet, spawnPos, Quaternion.identity, _data.Value.parent);
                    bullet.Init(systems.GetWorld());
                    bullet.PackEntity(entity);
                    bulletViewC.viewC.view = bullet.gameObject;
                    rendererC.renderer = bullet.GetComponent<Renderer>();
                    _positionPool.Value.Del(entity);
                }
            }
        }
    }
}