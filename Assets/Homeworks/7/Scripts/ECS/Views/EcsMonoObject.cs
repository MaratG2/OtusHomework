using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using UnityEngine;

namespace Homework7.Ecs.Views
{
    public abstract class EcsMonoObject : MonoBehaviour
    {
        protected EcsWorld _world;
        protected EcsPackedEntity packedEntity;

        public void Init(EcsWorld world) => _world = world;
        public void PackEntity(int entity) => packedEntity = _world.PackEntity(entity);
        public int GetEntity()
        {
            int entity = -1;
            if (packedEntity.Unpack(_world, out var gotEntity))
                entity = gotEntity;
            return entity;
        }
        
        protected virtual void OnFightAction
            (EcsMonoObject firstCollide, EcsMonoObject secondCollide)
        {
            if (_world == null)
                return;
            
            int entity = _world.NewEntity();
            var poolFightC = _world.GetPool<Fight_C>();
            ref var fightC = ref poolFightC.Add(entity);
            fightC.firstFighter = firstCollide;
            fightC.secondFighter = secondCollide;
        }
        
        protected virtual void OnBulletAction
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