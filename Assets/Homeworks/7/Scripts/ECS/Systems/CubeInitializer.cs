using Homework7.Ecs.Components.Block;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Homework7.Ecs.Systems 
{
    public struct CubeInitializer : IEcsInitSystem
    {
        private readonly EcsCustomInject<SharedData> _data;
        public void Init (IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            var poolCubeViews = world.GetPool<CubeView_C>();
            var poolDamage = world.GetPool<Damage_C>();
            var poolMovementSpeed = world.GetPool<MovementSpeed_C>();
            var poolReloadTime = world.GetPool<ReloadTime_C>();
            var poolDetectionDistance = world.GetPool<DetectionDistance_C>();
            var poolHealth = world.GetPool<Health_C>();
            var poolRenderers = world.GetPool<Renderer_C>();
            var poolColor = world.GetPool<Color_C>();
            
            for (int i = 0; i < _data.Value.countSpawn * 2; i++)
            {
                int entity = world.NewEntity();
                poolCubeViews.Add(entity);
                poolDamage.Add(entity).damage = _data.Value.damage;
                poolMovementSpeed.Add(entity).movementSpeed = _data.Value.movementSpeed;
                poolReloadTime.Add(entity).reloadTime = _data.Value.reloadTime;
                poolDetectionDistance.Add(entity).detectionDistance = _data.Value.detectionDistance;
                poolHealth.Add(entity).health = _data.Value.health;
                poolRenderers.Add(entity);
                poolColor.Add(entity).color = i < _data.Value.countSpawn ? _data.Value.colorBlue : _data.Value.colorRed;
            }
        }
    }
}