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

        private void Start () 
        {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _systems
                .Add(new CubeInitializer())
#if UNITY_EDITOR
                .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
                .Inject(_data)
                .Init ();
        }

        void Update () 
        {
            // process systems here.
            _systems?.Run ();
        }

        void OnDestroy () 
        {
            if (_systems != null) 
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy ();
                _systems = null;
            }
            
            // cleanup custom worlds here.
            
            // cleanup default world.
            if (_world != null) 
            {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}