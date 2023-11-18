using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Movement_C, Rigidbody_C>> _movementFilter;
        private readonly EcsPoolInject<RequireMove_C> _poolRequireMove;
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _movementFilter.Value)
            {
                ref var movementC = ref _movementFilter.Pools.Inc1.Get(entity);
                ref var rigidbodyC = ref _movementFilter.Pools.Inc2.Get(entity);

                if (_poolRequireMove.Value.Has(entity))
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