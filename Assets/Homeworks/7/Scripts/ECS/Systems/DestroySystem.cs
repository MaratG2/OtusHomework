using Homework7.Ecs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct DestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<View_C, RequireDeath_C>> _viewDeathFilter;
        private readonly EcsPoolInject<View_C> _viewPool;
        private readonly EcsPoolInject<RequireDeath_C> _requireDeathPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _viewDeathFilter.Value)
            {
                ref var viewC = ref _viewPool.Value.Get(entity);
                
                if (_requireDeathPool.Value.Has(entity))
                {
                    Object.DestroyImmediate(viewC.view);
                    systems.GetWorld().DelEntity(entity);
                }
            }
        }
    }
}