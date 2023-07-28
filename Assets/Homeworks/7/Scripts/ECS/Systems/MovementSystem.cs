using Homework7.Ecs.Components.Cube;
using Homework7.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CubeView_C, Movement_C, Team_C>> _cubeFilter;
        private readonly EcsPoolInject<CubeView_C> _poolCubeView;
        private readonly EcsPoolInject<Movement_C> _poolMovement;
        private readonly EcsPoolInject<Team_C> _poolTeam;
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _cubeFilter.Value)
            {
                ref CubeView_C cubeViewC = ref _poolCubeView.Value.Get(entity);
                ref Movement_C movementC = ref _poolMovement.Value.Get(entity);
                ref Team_C teamC = ref _poolTeam.Value.Get(entity);

                Vector3 direction;
                if (teamC.team == Team.Blue)
                    direction = new Vector3(1f, 0f, 0f);
                else
                    direction = new Vector3(-1f, 0f, 0f);

                if(movementC.isMoving)
                    cubeViewC.viewC.view.transform.position += 
                        direction * Time.deltaTime * movementC.movementSpeed;
            }
        }
    }
}