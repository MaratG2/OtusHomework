using Homework7.Ecs.Components;
using Homework7.Helpers;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Homework7.Ecs.Systems
{
    public class BorderCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<View_C>> _viewFilter;
        private readonly EcsPoolInject<View_C> _viewPool;
        private readonly EcsPoolInject<RequireDeath_C> _requireDeathPool;
        private readonly EcsCustomInject<WorldSO> _worldData;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _viewFilter.Value)
            {
                ref var viewC = ref _viewPool.Value.Get(entity);
                var viewPos = viewC.view.transform.position;
                
                if (CheckOutOfBounds.IsOut(viewPos, _worldData.Value))
                    _requireDeathPool.Value.Add(entity);
            }
        }
    }
}