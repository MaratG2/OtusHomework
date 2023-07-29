using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Homework7.Ecs.Systems
{
    public struct CheckHealthSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health_C>> _healthFilter;
        private readonly EcsPoolInject<Health_C> _healthPool;
        private readonly EcsPoolInject<RequireDeath_C> _requireDeathPool;
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _healthFilter.Value)
            {
                var healthC = _healthPool.Value.Get(entity);

                if (healthC.health <= 0)
                    _requireDeathPool.Value.Add(entity);
            }
        }
    }
}