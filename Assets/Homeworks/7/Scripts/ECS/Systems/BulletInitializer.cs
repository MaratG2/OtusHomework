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
        private readonly EcsFilterInject<Inc<Fight_C>> _fightFilter;
        private readonly EcsPoolInject<Fight_C> _fightPool;
        private readonly EcsPoolInject<Team_C> _teamPool;
        private readonly EcsCustomInject<SharedData> _data;
        private readonly EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _fightFilter.Value)
            {
                ref var fightC = ref _fightPool.Value.Get(entity);
                if (fightC.shootRequired)
                {
                    fightC.shootRequired = false;
                    SpawnBullet(fightC.firstFighter.gameObject);
                }
            }
        }

        private void SpawnBullet(GameObject origin)
        {
            var bulletViewPool = _world.Value.GetPool<BulletView_C>();
            var positionPool = _world.Value.GetPool<Position_C>();
            var movementPool = _world.Value.GetPool<Movement_C>();
            var rendererPool = _world.Value.GetPool<Renderer_C>();
            var colorPool = _world.Value.GetPool<Color_C>();
            
            int entity = _world.Value.NewEntity();
            
            bulletViewPool.Add(entity).isSpawn = true;
            positionPool.Add(entity).position = new Vector2(origin.transform.position.x, origin.transform.position.z);
            Team team = _teamPool.Value.Get(origin.GetComponent<EcsMonoObject>().GetEntity()).team;
            _teamPool.Value.Add(entity).team = team;
            movementPool.Add(entity);
            rendererPool.Add(entity);
            colorPool.Add(entity).color = team == Team.Blue ? _data.Value.colorBlue : _data.Value.colorRed;;
        }
    }
}