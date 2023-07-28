using Homework7.Ecs.Components.Bullet;
using Homework7.Ecs.Components.Cube;
using Homework7.Helpers;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public class BorderCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CubeView_C>> _cubeFilter;
        private readonly EcsFilterInject<Inc<BulletView_C>> _bulletFilter;
        private readonly EcsPoolInject<CubeView_C> _cubePool;
        private readonly EcsPoolInject<BulletView_C> _bulletPool;
        private readonly EcsCustomInject<SharedData> _data;
        private readonly EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cubeFilter.Value)
            {
                ref var cubeViewC = ref _cubePool.Value.Get(entity);
                var cubePos = cubeViewC.viewC.view.transform.position;
                if (CheckOutOfBounds.IsOut(cubePos, _data.Value))
                {
                    Object.DestroyImmediate(cubeViewC.viewC.view);
                    _world.Value.DelEntity(entity);
                }
            }
            
            foreach (int entity in _bulletFilter.Value)
            {
                ref var bulletViewC = ref _bulletPool.Value.Get(entity);
                var bulletPos = bulletViewC.viewC.view.transform.position;
                if (CheckOutOfBounds.IsOut(bulletPos, _data.Value))
                {
                    Object.DestroyImmediate(bulletViewC.viewC.view);
                    _world.Value.DelEntity(entity);
                }
            }
        }
    }
}