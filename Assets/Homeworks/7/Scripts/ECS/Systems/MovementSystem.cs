using Homework7.Ecs.Components.Block;
using Homeworks7;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CubeView_C, MovementSpeed_C, Team_C>> _cubeFilter;
        private readonly EcsPoolInject<CubeView_C> _poolCubeView;
        private readonly EcsPoolInject<MovementSpeed_C> _poolMovementSpeed;
        private readonly EcsPoolInject<Team_C> _poolTeam;
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cubeFilter.Value)
            {
                ref CubeView_C cubeViewC = ref _poolCubeView.Value.Get(entity);
                ref MovementSpeed_C movementSpeedC = ref _poolMovementSpeed.Value.Get(entity);
                ref Team_C teamC = ref _poolTeam.Value.Get(entity);

                Vector3 direction;
                if (teamC.team == Team.Blue)
                    direction = new Vector3(1f, 0f, 0f);
                else
                    direction = new Vector3(-1f, 0f, 0f);

                cubeViewC.viewC.view.transform.position += 
                    direction * Time.deltaTime * movementSpeedC.movementSpeed;
            }
        }
    }
}