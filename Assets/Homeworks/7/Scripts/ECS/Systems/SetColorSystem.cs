using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct SetColorSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Renderer_C, Color_C>> _rendererColorsFilter;
        private readonly EcsPoolInject<Renderer_C> _renderPool;
        private readonly EcsPoolInject<Color_C> _colorPool;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _rendererColorsFilter.Value)
            {
                ref Renderer_C rendererC = ref _renderPool.Value.Get(entity);
                ref Color_C colorC = ref _colorPool.Value.Get(entity);
                
                if(rendererC.renderer)
                    rendererC.renderer.material.color = colorC.color;
                else
                    Debug.LogWarning("No Renderer Attached");
            }
        }
    }
}