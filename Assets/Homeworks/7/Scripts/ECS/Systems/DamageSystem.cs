using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Homework7.Ecs.Systems
{
    public class DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Hit_C>> _hitFilter;
        private readonly EcsPoolInject<Hit_C> _hitPool;
        private readonly EcsPoolInject<Health_C> _healthPool;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _hitFilter.Value)
            {
                ref var hitC = ref _hitPool.Value.Get(entity);
                hitC.firstCollide.Delete();
                ref var healthC = ref _healthPool.Value.Get(hitC.secondCollide.GetEntity());
                healthC.health -= 1;
                _hitPool.Value.Del(entity);
            }
        }
    }
}