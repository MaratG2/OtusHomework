using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct CubeSpawnSystem : IEcsInitSystem
    {
        private EcsFilterInject<Inc<CubeView_C, Renderer_C, Position_C>> _filterCube;
        private readonly EcsCustomInject<SharedData> _data;
        public void Init(IEcsSystems systems)
        {
            var poolCubeViews = _filterCube.Pools.Inc1;
            var poolRenderers = _filterCube.Pools.Inc2;
            var poolPositions = _filterCube.Pools.Inc3;

            foreach (int entity in _filterCube.Value)
            {
                ref var cubeViewC = ref poolCubeViews.Get(entity);
                ref var cubeRendererC = ref poolRenderers.Get(entity);
                ref var cubePosition = ref poolPositions.Get(entity);

                Vector3 spawnPos = new Vector3(cubePosition.position.x, 0.5f, cubePosition.position.y);
                var newCube = Object.Instantiate
                    (_data.Value.prefab, spawnPos, Quaternion.identity, _data.Value.parent);
                cubeViewC.viewC.view = newCube;
                cubeRendererC.renderer = newCube.GetComponent<Renderer>();
            }
        }
    }
}