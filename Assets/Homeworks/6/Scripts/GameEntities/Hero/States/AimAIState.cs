using System;
using Atomic;
using Declarative;
using Homeworks6.Components;
using Homeworks6.Spawner;
using Homeworks6.Zombie;
using UnityEngine;

namespace Homeworks6.Hero.States
{
    public class AimAIState : IState
    {
        private readonly DeclarativeModel _model;
        
        public AimAIState(DeclarativeModel model)
        {
            _model = model;
        }

        public void Enter()
        {
            AimTowardsZombie();
            _model.onUpdate += AimTowardsZombie;
        }

        public void Exit()
        {
            _model.onUpdate -= AimTowardsZombie;
        }

        private void AimTowardsZombie(float dt)
        {
            var closestZombie = ChooseClosestZombie();
            if(closestZombie)
                _model.transform.LookAt(closestZombie.transform.position, Vector3.up);
        }
        
        private void AimTowardsZombie()
        {
            AimTowardsZombie(0f);
        }

        private ZombieEntity ChooseClosestZombie()
        {
            var aliveZombies = ZombieContainer.Zombies.FindAll
                (zombie => zombie.Get<IGetHPComponent>().GetHP() > 0);
            float minDistance = Single.PositiveInfinity;
            ZombieEntity closestZombie = null;
            foreach (var zombie in aliveZombies)
            {
                var distance = Vector3.Distance(zombie.transform.position, _model.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestZombie = zombie;
                }
            }
            return closestZombie;
        }
    }
}