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
        private readonly EcsPoolInject<Movement_C> _movementPool;
        private readonly EcsPoolInject<Weapon_C> _weaponPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _fightFilter.Value)
            {
                ref var fightC = ref _fightPool.Value.Get(entity);
                if(fightC.firstFighter == null || fightC.secondFighter == null)
                {
                    if (fightC.firstFighter)
                    {
                        ref var cleanMovementC = ref _movementPool.Value.Get(fightC.firstFighter.GetEntity());
                        ref var cleanWeaponC = ref _weaponPool.Value.Get(fightC.firstFighter.GetEntity());
                        
                        cleanWeaponC.hasTarget = false;
                        cleanMovementC.isMoving = true;
                        Debug.Log("END FIGHT, MOVE ON");
                    }
                    systems.GetWorld().DelEntity(entity);
                    continue;
                }
                
                int firstFighterEntity = fightC.firstFighter.GetEntity();
                ref var movementC = ref _movementPool.Value.Get(firstFighterEntity);
                ref var weaponC = ref _weaponPool.Value.Get(firstFighterEntity);

                movementC.isMoving = false;
                if (weaponC.reloadTimer < weaponC.reloadTime && !fightC.shootRequired)
                    weaponC.reloadTimer += Time.deltaTime;
                else
                {
                    weaponC.reloadTimer = 0f;
                    fightC.shootRequired = true;
                }
            }
        }
    }
}