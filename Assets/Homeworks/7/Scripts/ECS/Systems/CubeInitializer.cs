using System.Collections.Generic;
using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Homework7.Ecs.Systems 
{
    public struct CubeInitializer : IEcsInitSystem
    {
        private readonly EcsCustomInject<SpawnSO> _spawnData;
        private readonly EcsCustomInject<List<CubeSO>> _cubeDatas;
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
            
            for (int i = 0; i < _spawnData.Value.CountSpawn * 2; i++)
            {
                int cubeNumber = i < _spawnData.Value.CountSpawn ? 0 : 1;
                CubeSO cubeData = _cubeDatas.Value[cubeNumber];
                
                int entity = world.NewEntity();
                poolViews.Add(entity);
                poolDamage.Add(entity).damage = cubeData.Damage;
                poolMovement.Add(entity).movementSpeed = cubeData.MovementSpeed;
                poolWeapon.Add(entity).reloadTime = cubeData.ReloadTime;
                poolHealth.Add(entity).health = cubeData.Health.Value;
                poolRenderers.Add(entity);
                poolTeam.Add(entity).team =  cubeData.Team;
                poolColor.Add(entity).color = cubeData.Color;
                poolPosition.Add(entity);
                poolRigidbody.Add(entity);
            }
        }
    }
}