using System.Collections.Generic;
using Homework7.Ecs.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs
{
    public sealed class EcsStartup : MonoBehaviour 
    {
        [SerializeField] private WorldSO _worldData;
        [SerializeField] private SpawnSO _spawnData;
        [SerializeField] private List<CubeSO> _cubeDatas;
        
        private EcsWorld _world;        
        private IEcsSystems _systems;
        
        private void Start() 
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new CubeInitializer())
                .Add(new SetSpawnPositionSystem())
                .Add(new CubeSpawnSystem())
                .Add(new SetColorSystem())
                .Add(new MovementSystem())
                .Add(new FightSystem())
                .Add(new BulletInitializer())
                .Add(new BulletDirectionSystem())
                .Add(new BulletSpawnSystem())
                .Add(new DamageSystem())
                .Add(new CheckHealthSystem())
                .Add(new DestroySystem())
                .Add(new BorderCheckSystem())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .Inject(_worldData)
                .Inject(_spawnData)
                .Inject(_cubeDatas)
                .Init();
        }

        void Update() 
        {
            _systems?.Run();
        }

        void OnDestroy () 
        {
            if (_systems != null) 
            {
                _systems.Destroy();
                _systems = null;
            }
            
            if (_world != null) 
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}