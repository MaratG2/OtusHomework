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
        private readonly EcsFilterInject<Inc<Movement_C, Rigidbody_C>> _movementFilter;
        private readonly EcsPoolInject<Movement_C> _poolMovement;
        private readonly EcsPoolInject<Rigidbody_C> _poolRigidbody;
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _movementFilter.Value)
            {
                ref var movementC = ref _poolMovement.Value.Get(entity);
                ref var rigidbodyC = ref _poolRigidbody.Value.Get(entity);

                if (movementC.isMoving)
                {
                    Vector3 direction = new Vector3(movementC.direction.x, 0f, movementC.direction.y);
                    rigidbodyC.rigidbody.AddForce(direction * Time.deltaTime * movementC.movementSpeed);
                    rigidbodyC.rigidbody.velocity = Vector3.ClampMagnitude(rigidbodyC.rigidbody.velocity, 4f);
                }
                else
                    rigidbodyC.rigidbody.velocity = Vector3.zero;
            }
        }
    }
}