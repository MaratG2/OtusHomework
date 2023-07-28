using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct DestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CubeView_C, Health_C>> _cubeFilter;
        private readonly EcsPoolInject<CubeView_C> _cubeViewPool;
        private readonly EcsPoolInject<Health_C> _healthPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cubeFilter.Value)
            {
                ref var cubeViewC = ref _cubeViewPool.Value.Get(entity);
                var healthC = _healthPool.Value.Get(entity);

                if (healthC.health <= 0)
                {
                    Object.DestroyImmediate(cubeViewC.viewC.view);
                    systems.GetWorld().DelEntity(entity);
                }
            }
        }
    }
}