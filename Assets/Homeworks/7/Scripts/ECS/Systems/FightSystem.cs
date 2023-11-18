using Homework7.Ecs.Components;
using Homework7.Ecs.Components.Cube;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Homework7.Ecs.Systems
{
    public struct FightSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Fight_C>> _fightFilter;
        private readonly EcsPoolInject<Fight_C> _fightPool;
        private readonly EcsPoolInject<RequireMove_C> _requireMovePool;
        private readonly EcsPoolInject<Weapon_C> _weaponPool;
        private readonly EcsPoolInject<RequireShoot_C> _requireShootPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _fightFilter.Value)
            {
                ref var fightC = ref _fightPool.Value.Get(entity);
                if(fightC.firstFighter == null || fightC.secondFighter == null)
                {
                    if (fightC.firstFighter)
                    {
                        if(!_requireMovePool.Value.Has(fightC.firstFighter.GetEntity()))
                            _requireMovePool.Value.Add(fightC.firstFighter.GetEntity());
                    }
                    systems.GetWorld().DelEntity(entity);
                    continue;
                }
                
                int firstFighterEntity = fightC.firstFighter.GetEntity();
                ref var weaponC = ref _weaponPool.Value.Get(firstFighterEntity);

                if(_requireMovePool.Value.Has(firstFighterEntity))
                    _requireMovePool.Value.Del(firstFighterEntity);
                
                if (weaponC.reloadTimer < weaponC.reloadTime)
                    weaponC.reloadTimer += Time.deltaTime;
                else if(fightC.firstFighter != null && fightC.secondFighter != null)
                {
                    _requireShootPool.Value.Add(entity);
                    weaponC.reloadTimer = 0;
                }
            }
        }
    }
}