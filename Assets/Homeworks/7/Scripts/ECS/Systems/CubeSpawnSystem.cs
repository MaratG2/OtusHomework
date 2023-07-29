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
        private readonly EcsFilterInject<Inc<View_C, Rigidbody_C, Renderer_C, Position_C, Movement_C, Team_C>> _filterCube;
        private readonly EcsCustomInject<SharedData> _data;
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var poolViews = _filterCube.Pools.Inc1;
            var poolRigidbody = _filterCube.Pools.Inc2;
            var poolRenderers = _filterCube.Pools.Inc3;
            var poolPositions = _filterCube.Pools.Inc4;
            var poolMovement = _filterCube.Pools.Inc5;
            var poolTeam = _filterCube.Pools.Inc6;

            foreach (int entity in _filterCube.Value)
            {
                ref var viewC = ref poolViews.Get(entity);
                ref var rigidbodyC = ref poolRigidbody.Get(entity);
                ref var cubeRendererC = ref poolRenderers.Get(entity);
                ref var cubePosition = ref poolPositions.Get(entity);
                ref var cubeMovement = ref poolMovement.Get(entity);
                var cubeTeam = poolTeam.Get(entity);

                Vector3 spawnPos = new Vector3(cubePosition.position.x, 0.5f, cubePosition.position.y);
                var newCube = Object.Instantiate
                    (_data.Value.prefabCube, spawnPos, Quaternion.identity, _data.Value.parent);
                newCube.Init(world);
                newCube.PackEntity(entity);
                viewC.view = newCube.gameObject;
                rigidbodyC.rigidbody = newCube.gameObject.GetComponent<Rigidbody>();
                cubeRendererC.renderer = newCube.GetComponent<Renderer>();

                int direction = cubeTeam.team == Team.Blue ? 1 : -1;
                cubeMovement.direction = new Vector2(direction, 0f);
                cubeMovement.isMoving = true;
            }
        }
    }
}