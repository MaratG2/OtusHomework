using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct SetColorSystem : IEcsInitSystem
    {
        private EcsFilterInject<Inc<Renderer_C, Color_C>> _rendererColorsFilter;
        public void Init(IEcsSystems systems)
        {
            var poolRenderers = _rendererColorsFilter.Pools.Inc1;
            var poolColors = _rendererColorsFilter.Pools.Inc2;
            foreach (var entity in _rendererColorsFilter.Value)
            {
                ref Renderer_C rendererC = ref poolRenderers.Get(entity);
                ref Color_C colorC = ref poolColors.Get(entity);
                
                if(rendererC.renderer)
                    rendererC.renderer.material.color = colorC.color;
                else
                    Debug.LogWarning("No Renderer Attached");
            }
        }
    }
}