using Homework7.Ecs.Components.Bullet;
using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct BulletDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Bullet_C, Movement_C>> _bulletFilter;
        public void Run(IEcsSystems systems)
        {
            var poolBullets = _bulletFilter.Pools.Inc1;
            var poolMovement = _bulletFilter.Pools.Inc2;

            foreach (var entity in _bulletFilter.Value)
            {
                ref var movementC = ref poolMovement.Get(entity);
                if (movementC.direction != Vector2.zero)
                    continue;
                
                var bulletC = poolBullets.Get(entity);
                Vector3 direction3D = bulletC.target.transform.position - bulletC.shooter.transform.position;
                Vector2 direction = new Vector2(direction3D.x, direction3D.z).normalized;
                movementC.direction = direction;
            }
        }
    }
}