using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Bullet;
using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CubeView_C, Movement_C>> _movementCubeFilter;
        private readonly EcsFilterInject<Inc<BulletView_C, Movement_C>> _movementBulletFilter;
        private readonly EcsPoolInject<CubeView_C> _poolCubeView;
        private readonly EcsPoolInject<BulletView_C> _poolBulletView;
        private readonly EcsPoolInject<Movement_C> _poolMovement;
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _movementCubeFilter.Value)
            {
                ref var cubeViewC = ref _poolCubeView.Value.Get(entity);
                ref var movementC = ref _poolMovement.Value.Get(entity);

                if (movementC.isMoving)
                {
                    Vector3 direction = new Vector3(movementC.direction.x, 0f, movementC.direction.y);
                    cubeViewC.rigidbody.AddForce(direction * Time.deltaTime * movementC.movementSpeed);
                    cubeViewC.rigidbody.velocity = Vector3.ClampMagnitude(cubeViewC.rigidbody.velocity, 1f);
                }
                else
                    cubeViewC.rigidbody.velocity = Vector3.zero;
            }
            foreach (int entity in _movementBulletFilter.Value)
            {
                ref var bulletViewC = ref _poolBulletView.Value.Get(entity);
                ref var movementC = ref _poolMovement.Value.Get(entity);

                if(movementC.isMoving)
                {
                    Vector3 direction = new Vector3(movementC.direction.x, 0f, movementC.direction.y);
                    bulletViewC.viewC.view.transform.position +=
                        direction * Time.deltaTime * movementC.movementSpeed;
                }
            }
        }
    }
}