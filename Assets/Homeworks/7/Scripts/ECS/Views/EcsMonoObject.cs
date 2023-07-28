using Homework7.Ecs.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Homework7.Ecs.Views
{
    public abstract class EcsMonoObject : MonoBehaviour
    {
        protected EcsWorld _world;
        protected EcsPackedEntity packedEntity;
        public EcsPackedEntity PackedEntity => packedEntity;

        public void Init(EcsWorld world) => _world = world;
        public void PackEntity(int entity) => packedEntity = _world.PackEntity(entity);

        protected virtual void OnTriggerAction
            (EcsMonoObject firstCollide, EcsMonoObject secondCollide)
        {
            if (_world == null)
                return;
            int entity = _world.NewEntity();
            var poolHitC = _world.GetPool<Hit_C>();
            ref var hitC = ref poolHitC.Add(entity);
            hitC.firstCollide = firstCollide;
            hitC.secondCollide = secondCollide;
        }
    }
}