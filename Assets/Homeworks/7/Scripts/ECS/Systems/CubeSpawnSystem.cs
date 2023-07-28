using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Cube;
using Homework7.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct CubeSpawnSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<CubeView_C, Renderer_C, Position_C, Movement_C, Team_C>> _filterCube;
        private readonly EcsCustomInject<SharedData> _data;
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var poolCubeViews = _filterCube.Pools.Inc1;
            var poolRenderers = _filterCube.Pools.Inc2;
            var poolPositions = _filterCube.Pools.Inc3;
            var poolMovement = _filterCube.Pools.Inc4;
            var poolTeam = _filterCube.Pools.Inc5;

            foreach (int entity in _filterCube.Value)
            {
                ref var cubeViewC = ref poolCubeViews.Get(entity);
                ref var cubeRendererC = ref poolRenderers.Get(entity);
                ref var cubePosition = ref poolPositions.Get(entity);
                ref var cubeMovement = ref poolMovement.Get(entity);
                var cubeTeam = poolTeam.Get(entity);

                Vector3 spawnPos = new Vector3(cubePosition.position.x, 0.5f, cubePosition.position.y);
                var newCube = Object.Instantiate
                    (_data.Value.prefabCube, spawnPos, Quaternion.identity, _data.Value.parent);
                newCube.Init(world);
                newCube.PackEntity(entity);
                cubeViewC.viewC.view = newCube.gameObject;
                cubeViewC.rigidbody = newCube.gameObject.GetComponent<Rigidbody>();
                cubeRendererC.renderer = newCube.GetComponent<Renderer>();

                int direction = cubeTeam.team == Team.Blue ? 1 : -1;
                cubeMovement.direction = new Vector2(direction, 0f);
                cubeMovement.isMoving = true;
            }
        }
    }
}