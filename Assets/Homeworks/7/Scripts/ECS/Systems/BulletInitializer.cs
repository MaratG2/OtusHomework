using System.Collections.Generic;
using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Bullet;
using Homework7.Ecs.Components.Cube;
using Homework7.Ecs.Views;
using Homework7.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct BulletInitializer : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RequireShoot_C>> _requireShootFilter;
        private readonly EcsPoolInject<Fight_C> _fightPool;
        private readonly EcsPoolInject<Team_C> _teamPool;
        private readonly EcsCustomInject<List<CubeSO>> _cubeDatas;
        private readonly EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _requireShootFilter.Value)
            {
                var fightC = _fightPool.Value.Get(entity);
                if(fightC.firstFighter != null && fightC.secondFighter != null)
                {
                    SpawnBullet(fightC.firstFighter.gameObject, fightC.secondFighter.gameObject);
                    _requireShootFilter.Pools.Inc1.Del(entity);
                }
            }
        }

        private void SpawnBullet(GameObject origin, GameObject target)
        {
            var viewPool = _world.Value.GetPool<View_C>();
            var bulletPool = _world.Value.GetPool<Bullet_C>();
            var rigidbodyPool = _world.Value.GetPool<Rigidbody_C>();
            var positionPool = _world.Value.GetPool<Position_C>();
            var movementPool = _world.Value.GetPool<Movement_C>();
            var reqMovePool = _world.Value.GetPool<RequireMove_C>();
            var rendererPool = _world.Value.GetPool<Renderer_C>();
            var colorPool = _world.Value.GetPool<Color_C>();
            var requireSpawnPool = _world.Value.GetPool<RequireSpawn_C>();
            
            int entity = _world.Value.NewEntity();

            viewPool.Add(entity);
            ref var bulletC = ref bulletPool.Add(entity);
            bulletC.shooter = origin;
            bulletC.target = target;
            rigidbodyPool.Add(entity);
            requireSpawnPool.Add(entity);
            positionPool.Add(entity).position = new Vector2(origin.transform.position.x, origin.transform.position.z);
            Team team = _teamPool.Value.Get(origin.GetComponent<EcsMonoObject>().GetEntity()).team;
            CubeSO cubeData = _cubeDatas.Value[(int)team]; 
            _teamPool.Value.Add(entity).team = team;
            ref var movementC = ref movementPool.Add(entity);
            movementC.movementSpeed = cubeData.BulletSpeed;
            reqMovePool.Add(entity);
            rendererPool.Add(entity);
            colorPool.Add(entity).color = cubeData.Color;
        }
    }
}