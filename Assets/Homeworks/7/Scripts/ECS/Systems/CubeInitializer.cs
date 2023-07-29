using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Cube;
using Homework7.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems 
{
    public struct CubeInitializer : IEcsInitSystem
    {
        private readonly EcsCustomInject<SharedData> _data;
        public void Init (IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            var poolViews = world.GetPool<View_C>();
            var poolDamage = world.GetPool<Damage_C>();
            var poolMovement = world.GetPool<Movement_C>();
            var poolWeapon = world.GetPool<Weapon_C>();
            var poolHealth = world.GetPool<Health_C>();
            var poolRenderers = world.GetPool<Renderer_C>();
            var poolTeam = world.GetPool<Team_C>();
            var poolColor = world.GetPool<Color_C>();
            var poolPosition = world.GetPool<Position_C>();
            var poolRigidbody = world.GetPool<Rigidbody_C>();
            
            for (int i = 0; i < _data.Value.countSpawn * 2; i++)
            {
                int entity = world.NewEntity();
                poolViews.Add(entity);
                poolDamage.Add(entity).damage = _data.Value.damage;
                poolMovement.Add(entity).movementSpeed = _data.Value.movementSpeed;
                poolWeapon.Add(entity).reloadTime = _data.Value.reloadTime;
                poolHealth.Add(entity).health = Random.Range((int)_data.Value.healthRange.x, (int)_data.Value.healthRange.y);
                poolRenderers.Add(entity);
                poolTeam.Add(entity).team = i < _data.Value.countSpawn ? Team.Blue : Team.Red;
                poolColor.Add(entity).color = poolTeam.Get(entity).team == Team.Blue ? _data.Value.colorBlue : _data.Value.colorRed;
                poolPosition.Add(entity);
                poolRigidbody.Add(entity);
            }
        }
    }
}