using System;
using Atomic;
using Homeworks6.Components;
using Homeworks6.Hero;
using Homeworks6.Spawner;
using Homeworks6.Zombie;
using UnityEngine;
using Zenject;

namespace Homeworks6.Controllers
{
    public class ClosestZombieController : MonoBehaviour
    {
        private HeroEntity _heroEntity;
        private AtomicVariable<Entity> _heroTarget;
        private Transform _heroTransform;

        [Inject]
        private void Construct(HeroEntity heroEntity)
        {
            this._heroEntity = heroEntity;
        }

        private void Start()
        {
            _heroTransform = _heroEntity.Get<TransformComponent>().GetTransform();
            _heroTarget = _heroEntity.Get<TargetComponent>().GetTarget();
        }

        private void Update()
        {
            var closestZombie = ChooseClosestZombie();
            _heroTarget.Value = closestZombie;
        }

        private ZombieEntity ChooseClosestZombie()
        {
            var aliveZombies = ZombieContainer.Zombies.FindAll
                (zombie => zombie.Get<IGetHPComponent>().GetHP() > 0);
            float minDistance = Single.PositiveInfinity;
            ZombieEntity closestZombie = null;
            foreach (var zombie in aliveZombies)
            {
                var distance = Vector3.Distance(zombie.transform.position, _heroTransform.position);
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