using Homework7.Ecs.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs
{
    public sealed class EcsStartup : MonoBehaviour 
    {
        private EcsWorld _world;        
        private IEcsSystems _systems;
        [SerializeField] private SharedData _data;

        private void Start() 
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new CubeInitializer())
                .Add(new SetSpawnPositionSystem())
                .Add(new CubeSpawnSystem())
                .Add(new SetColorSystem())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .Add(new MovementSystem())
                .Inject(_data)
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
                _world.Destroy ();
                _world = null;
            }
        }
    }
}